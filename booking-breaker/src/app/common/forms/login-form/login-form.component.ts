import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthService} from './../../../API/services/auth/auth.service';

@Component({
    selector: 'app-login-form',
    templateUrl: './login-form.component.html',
    styleUrls: ['./login-form.component.scss'],
    providers: []
})
export class LoginFormComponent implements OnInit {
    errorMessageResources = {
        email: {
            required: 'Введите адрес почты.',
            pattern: 'Недействительный адрес почты.',
            invalidEmailOrPassword: '',
            serverError: ''
        },
        password: {
            required: 'Введите пароль.',
            invalidEmailOrPassword: 'Неправильный логин или пароль.',
            serverError: 'Ошибка сервера'
        }
    };
    loginForm: FormGroup;
    email: '';
    password: '';

    submitted = false;

    constructor(
        private formBuilder: FormBuilder,
        private authService: AuthService
    ) {}
    ngOnInit() {
        this.buildLoginForm();
    }

    buildLoginForm() {
        this.loginForm = this.formBuilder.group({
            email: [this.email, Validators.compose([
                Validators.required,
                Validators.pattern(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)
            ])
            ],
            password: [this.password, Validators.compose([
                Validators.required
            ])
        ]
        });
    }

    onSubmitLogin() {
        this.submitted = true;
        this.authService.login(this.loginForm.value.email, this.loginForm.value.password)
        .catch(response => {
            if (response === 'The user name or password is incorrect.') {
                this.loginForm.setErrors({'invalidEmailOrPassword': true});
                this.loginForm.controls['password'].setErrors({
                    'invalidEmailOrPassword': true
                  });
                  this.loginForm.controls['email'].setErrors({
                    'invalidEmailOrPassword': true
                  });
            } else {
                this.loginForm.controls['password'].setErrors({
                    'serverError': true
                  });
                  this.loginForm.controls['email'].setErrors({
                    'serverError': true
                  });
            }
            const subscriptionEmail = this.loginForm.get('email').valueChanges.subscribe(() => {
                    subscriptionEmail.unsubscribe();
                    subscriptionPassword.unsubscribe();
                this.loginForm.controls['email'].updateValueAndValidity();
                this.loginForm.controls['password'].updateValueAndValidity();
              });

              const subscriptionPassword = this.loginForm.get('password').valueChanges.subscribe(() => {
                    subscriptionEmail.unsubscribe();
                    subscriptionPassword.unsubscribe();
                    this.loginForm.controls['email'].updateValueAndValidity();
                    this.loginForm.controls['password'].updateValueAndValidity();
              });

            this.submitted = false;
        });
    }
}
