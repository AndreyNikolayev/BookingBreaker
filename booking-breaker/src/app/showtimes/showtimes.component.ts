import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

import { Showtime } from './../API/models/showtime';
import { ShowtimeService } from './../API/services//showtime/showtime.service';

@Component({
    selector: 'app-showtimes',
    templateUrl: './showtimes.component.html',
    styleUrls: ['./showtimes.component.scss'],
    providers: []
  })

  export class ShowTimesComponent implements OnInit {

    showtimes: Showtime[];

    constructor(
      private showtimeService: ShowtimeService,
      private router: Router
    ) {}

    getShowtimes(): void {
        this.showtimeService.getShowtimes()
        .then(showtimes => this.showtimes = showtimes);
    }

    ngOnInit() {
      this.getShowtimes();
    }

    openPlaces(showId: number): void {
        this.router.navigate(['/showplaces', showId]);
    }
  }
