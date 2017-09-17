import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {ActivatedRoute, ParamMap} from '@angular/router';
import {Location} from '@angular/common';

import {Showtime} from './../API/models/showtime';
import { ShowtimePlace } from './../API/models/showtime-place';
import { ShowtimeService } from './../API/services/showtime/showtime.service';
import {ShowplaceService} from './../API/services/showplace/showplace.service';

import { ShowtimePlaceStyleDirective } from './../shared/directives/style-directives/showplace-style-directive/showplace-style.directive';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'app-showplaces',
    templateUrl: './showplaces.component.html',
    styleUrls: ['./showplaces.component.scss'],
    providers: []
  })

  export class ShowPlacesComponent implements OnInit {

    showplaces: ShowtimePlace[];
    showtime: Showtime;

    relativeDivMinHeightPixel: string;

    constructor(
      private showtimeService: ShowtimeService,
      private showplaceService: ShowplaceService,
      private route: ActivatedRoute,
      private location: Location
    ) {}


    ngOnInit() {
        this.route.paramMap
        .switchMap((params: ParamMap) =>
       this.showtimeService.getShowtime(+params.get('showtimeid')))
       .subscribe(showtime => this.showtime = showtime);

        this.route.paramMap
        .switchMap((params: ParamMap) =>
       this.showplaceService.getShowplaces(+params.get('showtimeid')))
       .subscribe(showplaces => {
         this.showplaces = showplaces;
         this.relativeDivMinHeightPixel = Math.max.apply(null, this.showplaces.map((showplace, index, array) =>
            showplace.ShowTimePlaceStyle.Top + showplace.ShowTimePlaceStyle.Height + 10)) + 'px';
        });
    }

    goBack(): void {
        this.location.back();
    }
  }
