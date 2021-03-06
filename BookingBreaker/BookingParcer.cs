﻿using BookingBreakerBusinessLogic;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BookingBreaker
{
    public partial class BookingParcer : ServiceBase
    {
        private static Logger _logger = LogManager.GetLogger("BookingBreaker");

        public BookingParcer()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _logger.Info("Service Starting");

            System.Timers.Timer showTimeTimer = new System.Timers.Timer();
            showTimeTimer.Interval = 1800000; // 30min
            showTimeTimer.AutoReset = true;
            showTimeTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnShowTimeTimer);
            showTimeTimer.Start();

            System.Timers.Timer movieTimer = new System.Timers.Timer();
            movieTimer.Interval = 1800000; // 1 month
            movieTimer.AutoReset = true;
            movieTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnMovieTimer);
            movieTimer.Start();

            _logger.Info("Service Started");
        }

        protected override void OnStop()
        {
            _logger.Info("Service Stopped");
        }

        public void OnMovieTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            _logger.Info("Service Start Movie Parcing");
            PlanetaParcingBusinessLogic.ExecuteMovieParcing().ConfigureAwait(false).GetAwaiter().GetResult();
            _logger.Info("Service Stop Movie Parcing");
        }


        public void OnShowTimeTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            _logger.Info("Service Start showtimes parcing");
            PlanetaParcingBusinessLogic.ExecuteShowTimesParcing().ConfigureAwait(false).GetAwaiter().GetResult();
            _logger.Info("Service Stop Showtimes parcing");

            _logger.Info("Service Start subscriptions sending");
            SubscriptionBusinessLogic.CheckStartSubscriptionsSend();
            _logger.Info("Service Stop subscriptions sending");
        }
    }
}
