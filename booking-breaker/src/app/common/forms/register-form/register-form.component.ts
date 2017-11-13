import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthService} from './../../../API/services/auth/auth.service';

@Component({
    selector: 'app-register-form',
    templateUrl: './register-form.component.html',
    styleUrls: ['./register-form.component.scss'],
    providers: []
})
export class RegisterFormComponent implements OnInit {
    errorMessageResources = {
        username: {
            required: 'Введите имя пользователя.',
            pattern: 'Используйте только латинские буквы и цифры.',
            taken: 'Это имя пользователя уже занято'
        },
        email: {
            required: 'Введите email.',
            pattern: 'Недействительный email.',
            taken: 'Этот email уже занят.',
        },
        password: {
            required: 'Введите пароль.',
            serverError: 'Ошибка сервера',
            minlength: 'Минимальная длина пароля - 6 символов.'
        }
    };
    registerForm: FormGroup;
    email: '';
    password: '';
    username: '';

    submitted = false;

    constructor(
        private formBuilder: FormBuilder,
        private authService: AuthService
    ) {}
    ngOnInit() {
        this.buildRegisterForm();
    }

    buildRegisterForm() {
        this.registerForm = this.formBuilder.group({
            email: [this.email, Validators.compose([
                Validators.required,
                Validators.pattern(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)
            ])
        ],
            password: [this.password, Validators.compose([
                Validators.required,
                Validators.minLength(6)
            ])
        ],
            username: [this.username, Validators.compose([
                Validators.required,
                Validators.pattern(/^[a-zA-Z0-9]*$/)
            ])
        ]
        });
    }

    onSubmitRegister() {
        this.submitted = true;
        this.authService.register(this.registerForm.value.email, this.registerForm.value.username, this.registerForm.value.password)
        .catch(response => {
            if (response === 'Ошибка сервера') {
                this.registerForm.controls['password'].setErrors({
                    'serverError': true
                  });
            } else {
                response[''].forEach((element: string) => {
                    if (element.indexOf('Name') === 0) {
                        this.registerForm.controls['username'].setErrors({
                            'taken': true
                          });
                    } else if (element.indexOf('Email') === 0) {
                        this.registerForm.controls['email'].setErrors({
                            'taken': true
                          });
                    }
                });
            }
            const subscriptionEmail = this.registerForm.get('email').valueChanges.subscribe(() => {
                    subscriptionEmail.unsubscribe();
                    subscriptionPassword.unsubscribe();
                this.registerForm.controls['email'].updateValueAndValidity();
                this.registerForm.controls['password'].updateValueAndValidity();
              });

              const subscriptionPassword = this.registerForm.get('password').valueChanges.subscribe(() => {
                    subscriptionEmail.unsubscribe();
                    subscriptionPassword.unsubscribe();
                    this.registerForm.controls['email'].updateValueAndValidity();
                    this.registerForm.controls['password'].updateValueAndValidity();
              });

            this.submitted = false;
        });
    }
}
