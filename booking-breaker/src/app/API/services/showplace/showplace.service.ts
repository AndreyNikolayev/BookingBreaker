import {Injectable} from '@angular/core';
import { Headers, Http } from '@angular/http';
import { environment } from './../../../../environments/environment';

import 'rxjs/add/operator/toPromise';
import { ShowtimePlace } from './../../models/showtime-place';

@Injectable()
export class ShowplaceService {
    private showplacesUrl = environment.base_url + 'api/showplaces';

    constructor(private http: Http) { }

    getShowplaces(showtimeId: number): Promise<ShowtimePlace[]> {
        return this.http.get(this.showplacesUrl + '/' + showtimeId)
        .toPromise()
        .then(
            response => response.json() as ShowtimePlace[]
    )
        .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
      }
}
