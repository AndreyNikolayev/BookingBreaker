import { Component, ComponentRef, OnInit, OnDestroy} from '@angular/core';
import { MzModalService } from 'ng2-materialize';
import { LoginModalComponent } from './../modals/login-modal/login-modal.component';
import { MzBaseModal} from 'ng2-materialize';

import {AuthService} from './../../API/services/auth/auth.service';

import {EventAggregator} from './../../event-aggregator/event-aggregator';
import {UserAuthChangedEvent} from './../../event-aggregator/events/user-auth-changed.event';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss'],
    providers: []
  })

  export class NavbarComponent implements OnInit, OnDestroy {
    modalComponent: ComponentRef<MzBaseModal>;

    isLoggedIn(): boolean {
        return this.authService.isLoggedIn();
    }

    constructor(
        private modalService: MzModalService,
        private authService: AuthService,
        private eventAggregator: EventAggregator
    ) { }

    onAuthChanged(isLoggedIn: boolean, modal: ComponentRef<MzBaseModal>) {
      if (isLoggedIn) {
        modal.instance.modalComponent.close();
      }
    }

    ngOnInit() {
        this.eventAggregator.getEvent(UserAuthChangedEvent).subscribe(x => this.onAuthChanged(x, this.modalComponent));
    }

    ngOnDestroy() {
      this.eventAggregator.getEvent(UserAuthChangedEvent).unsubscribe(x => this.onAuthChanged(x, this.modalComponent));
    }

    public openLoginModal() {
        this.modalComponent = this.modalService.open(LoginModalComponent);
        console.log(this.modalComponent);
    }

    public logout() {
        this.authService.logout();
    }
  }
