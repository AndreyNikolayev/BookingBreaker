import { Component } from '@angular/core';
import { MzBaseModal } from 'ng2-materialize';

@Component({
    selector: 'app-auth-modal',
    templateUrl: './login-modal.component.html',
    styleUrls: ['./login-modal.component.scss'],
    providers: []
})
export class LoginModalComponent extends MzBaseModal {
    showLoginForm = true;

    goToLogin() {
        this.showLoginForm = true;
    }

    goToRegister() {
        this.showLoginForm = false;
    }
}
