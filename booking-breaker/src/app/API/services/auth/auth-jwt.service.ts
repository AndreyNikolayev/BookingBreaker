import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { isNull } from 'util';

// -- Custom Providers
import { StorageService } from '../data-storage/storage.service';

// -- Custom Models
import { User } from '../../models/user';


@Injectable()
export class AuthJwtService {

  constructor(private storageService: StorageService , private http: Http) {}

  setToken(idToken: string): void {
    this.storageService.setToken(idToken);
  }

  getToken(): string {
    return this.storageService.getToken();
  }

  setCurrentUser(user: User): void {
    this.storageService.setCurrentUser(user);
  }

  getCurrentUser(): User {
    return this.storageService.getCurrentUser();
  }

  isLoggedIn(): boolean {
    return !isNull(this.getToken());
  }

  logout(): void {
    this.storageService.clearAll();
  }
}
