import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

import { Showtime } from './../API/models/showtime';
import {ShowtimeView} from './../API/view-models/showtime-view';
import { ShowtimeService } from './../API/services//showtime/showtime.service';

@Component({
    selector: 'app-showtimes',
    templateUrl: './showtimes.component.html',
    styleUrls: ['./showtimes.component.scss'],
    providers: []
  })

  export class ShowTimesComponent implements OnInit {

    showtimeViews: ShowtimeView[];

    constructor(
      private showtimeService: ShowtimeService,
      private router: Router
    ) {}

    isActive(showtime: Showtime): boolean {
        return new Date(showtime.StartTime).getTime() > new Date().getTime();
    }

    getShowtimes(): void {
        this.showtimeService.getShowtimes()
        .then(showtimeViews => this.showtimeViews = showtimeViews)
        .then(response => console.log(this.showtimeViews[0].Movies[0]));
    }

    ngOnInit() {
      this.getShowtimes();
    }

    openPlaces(showId: number): void {
        this.router.navigate(['/showplaces', showId]);
    }
  }
