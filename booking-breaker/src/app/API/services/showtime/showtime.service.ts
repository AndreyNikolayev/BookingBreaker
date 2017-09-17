import {Injectable} from '@angular/core';
import { Headers, Http } from '@angular/http';
import { environment } from './../../../../environments/environment';

import 'rxjs/add/operator/toPromise';
import { Showtime } from './../../models/showtime';

@Injectable()
export class ShowtimeService {
    private showtimesUrl = environment.base_url + 'api/showtimes';

    constructor(private http: Http) { }

    getShowtimes(): Promise<Showtime[]> {
        return this.http.get(this.showtimesUrl)
        .toPromise()
        .then(
            response => response.json() as Showtime[]
    )
        .catch(this.handleError);
    }

    getShowtime(id: number): Promise<Showtime> {
        return this.http.get(this.showtimesUrl + '/' + id)
        .toPromise()
        .then(
            response => response.json() as Showtime[]
    )
        .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
      }
}
