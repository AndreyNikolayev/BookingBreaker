import { Injectable } from '@angular/core';
import { LocalStorageService } from 'angular-2-local-storage';

import {User} from './../../models/user';

// -- Define keys that needs to go into localstorage here
const STORAGE_KEYS = {
  USER_TOKEN: 'user_token',
  USER: 'user'
};

/*
* This provider handles all the operations related to localstorage
* */

@Injectable()
export class StorageService {

  constructor(private localStorageService: LocalStorageService) {
  }

  setToken(idToken: string): void {
    this.localStorageService.set(STORAGE_KEYS.USER_TOKEN, idToken);
  }

  getToken(): string {
    return this.localStorageService.get<string>(STORAGE_KEYS.USER_TOKEN);
  }

  removeToken(): void {
    this.localStorageService.remove(STORAGE_KEYS.USER_TOKEN);
  }

  setCurrentUser(user: User): void {
    this.localStorageService.set(STORAGE_KEYS.USER, user);
  }

  getCurrentUser(): User {
    return this.localStorageService.get<User>(STORAGE_KEYS.USER);
  }

  clearAll() {
    this.localStorageService.clearAll();
  }

}

