using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Xml.Linq;
using BookingDataAccess;
using System.Data.Entity;
using AngleSharp.Parser.Html;

namespace BookingBreakerBusinessLogic
{
    public class PlanetaParcingBusinessLogic
    {
        private static Logger _logger = LogManager.GetLogger("BookingBreaker");

        private static object _locker = new object();

        private static HttpClient _client = new HttpClient();

        private static double ConvertStyleAttrToDouble(string key, Dictionary<string,string> dictionary)
        {
            return Convert.ToDouble(dictionary[key].Substring(0, dictionary[key].Length - 2));
        }

        public static List<ShowTimePlace> ParseShowTime(string htmlPage, CinemaHall cinemaHall)
        {
            var result = new List<ShowTimePlace>();

            var parser = new HtmlParser();
            var document = parser.Parse(htmlPage);

            var all = document.All.Where(p => p.LocalName == "div");

            var placesHtml = document.All.Where(p => p.LocalName == "div" && p.ClassList.Any(n => n.Contains("hs-image-000000")));

            var rowsPlaceCss = new Dictionary<double, List<double>>();

            if(!placesHtml.Any())
            {
                return null;
            }

            foreach (var placeHtml in placesHtml)
            {
                var placeHtmlStyle = placeHtml.GetAttribute("style");
                var inlineStyleDictionary = new Dictionary<string, string>();
                foreach (var styleAttr in placeHtmlStyle.Replace(" ", "").ToLower().Split(';').ToList())
                {
                    if(styleAttr == "")
                    {
                        continue;
                    }
                    var splitAttr = styleAttr.Split(':');
                    inlineStyleDictionary[splitAttr[0]] = splitAttr[1];
                }
                var showtimePlaceStyle = new ShowTimePlaceStyle {
                    Left = ConvertStyleAttrToDouble("left", inlineStyleDictionary),
                    Top = ConvertStyleAttrToDouble("top", inlineStyleDictionary),
                    Width = ConvertStyleAttrToDouble("width", inlineStyleDictionary),
                    Height = ConvertStyleAttrToDouble("height", inlineStyleDictionary)
                };
                if (!rowsPlaceCss.ContainsKey(showtimePlaceStyle.Top))
                {
                    rowsPlaceCss.Add(showtimePlaceStyle.Top, new List<double> { showtimePlaceStyle.Left });
                }
                else
                {
                    rowsPlaceCss[showtimePlaceStyle.Top].Add(showtimePlaceStyle.Left);
                }

                var placeAccess = placeHtml.ClassList.Contains("hs-image-0000000007") ? PlaceAccessEnum.Disabled : PlaceAccessEnum.Open;



                var showtimePlace = new ShowTimePlace
                {
                    PlaceAccess = placeHtml.ClassList.Contains("hs-image-0000000005") || placeHtml.ClassList.Contains("hs-image-0000000040") ? PlaceAccessEnum.Taken : placeAccess,
                    ShowTimePlaceStyle = showtimePlaceStyle
                };
                result.Add(showtimePlace);
            }
            var rowOrder = rowsPlaceCss.Keys.ToList();

            rowOrder.Sort();

            foreach (var places in rowsPlaceCss.Values)
            {
                places.Sort();
            }

            if (cinemaHall.VerticalHallLayout == VerticalHallLayoutEnum.None)
            {
                cinemaHall.VerticalHallLayout = VerticalHallLayoutEnum.FirstToLast;
            }
            if (cinemaHall.HorizontalHallLayout == HorizontalHallLayoutEnum.None)
            {
                var checkingPlaceFromResult = result.FirstOrDefault(p => p.PlaceAccess == PlaceAccessEnum.Open &&
                (rowsPlaceCss[p.ShowTimePlaceStyle.Top].Count % 2 == 0 ||
                p.ShowTimePlaceStyle.Left != rowsPlaceCss[p.ShowTimePlaceStyle.Top].ElementAt(rowsPlaceCss[p.ShowTimePlaceStyle.Top].Count - 1 / 2)));

                if (checkingPlaceFromResult == null)
                {
                    cinemaHall.HorizontalHallLayout = HorizontalHallLayoutEnum.LeftToRight;
                }
                else
                {
                    var checkPlaceFoundNumber = rowsPlaceCss[checkingPlaceFromResult.ShowTimePlaceStyle.Top].IndexOf(checkingPlaceFromResult.ShowTimePlaceStyle.Left) + 1;

                    var checkingPlaceFromHtml = placesHtml.FirstOrDefault(p => Double.Parse(p.OuterHtml.Substring(p.OuterHtml.IndexOf("px;top:") + 7, p.OuterHtml.IndexOf("px;width:") - p.OuterHtml.IndexOf("px;top:") - 7)) == checkingPlaceFromResult.ShowTimePlaceStyle.Top &&
                    Double.Parse(p.OuterHtml.Substring(p.OuterHtml.IndexOf("\"left:") + 6, p.OuterHtml.IndexOf("px;top:") - p.OuterHtml.IndexOf("\"left:") - 6)) == checkingPlaceFromResult.ShowTimePlaceStyle.Left);

                    _logger.Info(checkingPlaceFromHtml.GetAttribute("exp-data-col"));
                    var checkingPlaceFromHtmlPlaceNumber = Int32.Parse(checkingPlaceFromHtml.GetAttribute("exp-data-col"));


                    cinemaHall.HorizontalHallLayout = checkingPlaceFromHtmlPlaceNumber == checkPlaceFoundNumber ? HorizontalHallLayoutEnum.LeftToRight : HorizontalHallLayoutEnum.RightToLeft;
                }
            }

            if (cinemaHall.VerticalHallLayout == VerticalHallLayoutEnum.LastToFirst)
            {
                rowOrder.Reverse();
            }

            if (cinemaHall.HorizontalHallLayout == HorizontalHallLayoutEnum.RightToLeft)
            {
                foreach (var places in rowsPlaceCss.Values)
                {
                    places.Reverse();
                }
            }

            foreach (var showtimePlace in result)
            {
                showtimePlace.PlaceNumber = rowsPlaceCss[showtimePlace.ShowTimePlaceStyle.Top].IndexOf(showtimePlace.ShowTimePlaceStyle.Left) + 1;
                showtimePlace.Row = rowOrder.IndexOf(showtimePlace.ShowTimePlaceStyle.Top) + 1;
            }

            return result;
        }

        public static async Task ExecuteShowTimesParcing()
        {
            try
            {
                var response = await _client.GetAsync(@"https://planetakino.ua/showtimes/xml");

                var responseString = await response.Content.ReadAsStringAsync();
                var xmlDoc = XDocument.Parse(responseString);
                var showtimes = xmlDoc.Descendants("showtimes").Descendants("day").Descendants("show");

                using (BookingBreakerContext db = new BookingBreakerContext())
                {
                    var cinema = GetParcingCinema(db);

                    db.ShowTimes.RemoveRange(db.ShowTimes.Where(p => p.CinemaHall.Cinema.Title == cinema.Title));

                    foreach (var show in showtimes)
                    {
                        var movieId = show.Attribute("movie-id").Value;
                        var startTime = DateTime.Parse(show.Attribute("full-date").Value);
                        var url = show.Attribute("order-url").Value;
                        var hallId = show.Attribute("hall-id").Value;
                        var technology = show.Attribute("technology").Value;
                        if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(movieId))
                        {
                            continue;
                        }
                        var hallIdInt = Int32.Parse(hallId);
                        var cinemaHall = db.CinemaHalls.FirstOrDefault(p => p.LocalCinemaHallId == hallIdInt);
                        if (cinemaHall == null)
                        {
                            cinemaHall = new CinemaHall();
                            cinemaHall.LocalCinemaHallId = Int32.Parse(hallId);
                            cinemaHall.PlacesRepresentationTech = PlacesRepresentationTechEnum.SeatCharts;
                            cinemaHall.Cinema = cinema;
                            db.CinemaHalls.Add(cinemaHall);
                        }


                        var showtimeId = url.Substring(url.IndexOf("show_id=") + 8, url.IndexOf(@"&theatre_id") - url.IndexOf("show_id") - 8);

                        var showresponse = await _client.GetAsync(@"https://cabinet.planetakino.ua" + "/Hall/HallScheme?" + "showtimeId=" + showtimeId + "&theaterId=imax-kiev&hallId=" + hallId + "&attrTechnology=" + technology + "&transactionId=");
                        var placesContent = await showresponse.Content.ReadAsStringAsync();
                        var showPlaces = ParseShowTime(placesContent, cinemaHall);

                        if(showPlaces == null)
                        {
                            continue;
                        }

                        var showTimeTech = TechnologyEnum.TwoD;

                        if (technology.ToLower().Contains("3d"))
                        {
                            showTimeTech = TechnologyEnum.ThreeD;
                        }
                        if (technology.ToLower().Contains("imax"))
                        {
                            showTimeTech = TechnologyEnum.IMAX;
                        }

                        var showtimeEntity = new ShowTime
                        {
                            CinemaHall = cinemaHall,
                            Movie = db.LocalMovieIdentities.Include(c => c.Movie).First(p => p.LocalIdentifier == movieId).Movie,
                            StartTime = startTime,
                            Link = url,
                            ShowTimePlaces = showPlaces,
                            Technology = showTimeTech
                        };
                        showPlaces.ForEach(p => p.ShowTime = showtimeEntity);

                        db.ShowTimes.Add(showtimeEntity);
                        db.SaveChanges();
                    };

                    db.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                _logger.Warn("Exception: " + ex.Message);
                _logger.Warn("Exception Trace: " + ex.StackTrace);
            }
        }

        private static Cinema GetParcingCinema(BookingBreakerContext db)
        {
            var cinema = db.Cinemas.FirstOrDefault(p => p.Title == "Планета Кино");
            if (cinema == null)
            {
                cinema = new Cinema { Title = "Планета Кино" };
                db.Cinemas.Add(cinema);
            }

            return cinema;
        }


        public static async Task ExecuteMovieParcing()
        {
            _logger.Info("Start Movie Parcing");

            try
            {
                var response = await _client.GetAsync(@"https://planetakino.ua/showtimes/xml");

                var responseString = await response.Content.ReadAsStringAsync();
                var xmlDoc = XDocument.Parse(responseString);
                var films = xmlDoc.Descendants("movies").Descendants("movie");
                var showtimes = xmlDoc.Descendants("showtimes").Descendants("day").Descendants("show");

                using (BookingBreakerContext db = new BookingBreakerContext())
                {
                    var cinema = GetParcingCinema(db);

                    foreach (var film in films)
                    {
                        var filmTitle = film.Descendants("title").First().Value;
                        var startDate = film.Descendants("dt-start").First().Value;
                        var endDate = film.Descendants("dt-end").First().Value;

                        var cinemaMovieId = film.Attribute("id").Value;
                        var cinemaMovieLink = film.Attribute("url").Value;



                        var movie = db.Movies.FirstOrDefault(p => p.Title == filmTitle);
                        if (movie == null)
                        {
                            movie = new Movie { Title = filmTitle };
                            db.Movies.Add(movie);
                        }



                        var showDuration = db.ShowDurations.FirstOrDefault(p => p.Movie.Title == movie.Title && p.Cinema.Title == cinema.Title);

                        if (startDate.Contains("0000"))
                        {
                            continue;
                        }
                        if(endDate.Contains("0000"))
                        {
                            endDate = DateTime.Now.AddYears(1).ToShortDateString();
                        }

                        if (showDuration == null)
                        {
                            showDuration = new ShowDuration();
                            db.ShowDurations.Add(showDuration);
                        }
                        showDuration.StartShowDate = DateTime.Parse(startDate);
                        showDuration.EndShowDate = DateTime.Parse(endDate);
                        showDuration.Cinema = cinema;
                        showDuration.Movie = movie;

                        var localMovieIdentity = db.LocalMovieIdentities.FirstOrDefault(p => p.Movie.Title == movie.Title && p.Cinema.Title == cinema.Title);
                        if (localMovieIdentity == null)
                        {
                            localMovieIdentity = new LocalMovieIdentity();
                            db.LocalMovieIdentities.Add(localMovieIdentity);
                        }

                        localMovieIdentity.LocalMovieLink = cinemaMovieLink;
                        localMovieIdentity.LocalIdentifier = cinemaMovieId;
                        localMovieIdentity.Cinema = cinema;
                        localMovieIdentity.Movie = movie;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.Warn("Exception: " + ex.Message);
                _logger.Warn("Exception Trace: " + ex.StackTrace);
            }

            _logger.Info("End Movie Parcing");
        }
    }
}
