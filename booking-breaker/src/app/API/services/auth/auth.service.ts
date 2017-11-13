import { Injectable } from '@angular/core';
import { Http, Headers, URLSearchParams } from '@angular/http';

import {AuthJwtService} from './auth-jwt.service';

import {EventAggregator} from './../../../event-aggregator/event-aggregator';
import {UserAuthChangedEvent} from './../../../event-aggregator/events/user-auth-changed.event';

// -- Custom Models
import { User } from '../../models/user';

import { environment } from './../../../../environments/environment';

@Injectable()
export class AuthService {

  constructor(private http: Http, private authJwtService: AuthJwtService, private eventAggregator: EventAggregator) {}

    isLoggedIn(): boolean {
        return this.authJwtService.isLoggedIn();
    }

    logout() {
        this.authJwtService.logout();
        this.eventAggregator.getEvent(UserAuthChangedEvent).publish(false);
    }

    register(email: string, username: string, password: string) {
        const headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });

        const params = new URLSearchParams();
        params.set('Email', email);
        params.set('Username', username);
        params.set('password', password);

        return this.http.post(environment.base_url + 'api/Account/Register',
        params.toString(), {headers: headers})
        .toPromise()
        .then(response => {
                this.login(email, password);
        })
        .catch(response => {
            if (response.json().ModelState) {
            return Promise.reject(response.json().ModelState);
            }
            return Promise.reject('Ошибка сервера');
        });
    }

    login(email: string, password: string) {
        const headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });

        const loginData = {
            grant_type: 'password',
            username: email,
            password: password
        };

        const params = new URLSearchParams();
        params.set('grant_type', 'password');
        params.set('username', email);
        params.set('password', password);

        return this.http.post(environment.base_url + 'token',
        params.toString(), {headers: headers})
        .toPromise()
        .then(
            response => {
                this.authJwtService.setToken('Bearer ' + response.json().access_token);
                this.eventAggregator.getEvent(UserAuthChangedEvent).publish(true);
                return Promise.resolve();
            }
        )
        .catch(response => {
            console.log(response);
            if (response.status !== 400) {
                return Promise.reject('Ошибка сервера');
            }
            const responseObject = response.json();
            return Promise.reject(responseObject.error_description || responseObject.error);
        });
    }
  }
