import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IAuthService } from 'src/app/core/services/IAuth.service';
import { SpinnerService } from 'src/app/service/spinner.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
    constructor(private authService: IAuthService, private router: Router,
        public spinnerService: SpinnerService) { }

    error: string;

    ngOnInit(): void {
    }

    form: FormGroup = new FormGroup({
        username: new FormControl('murat.dere'),
        password: new FormControl('Password5*'),
    });


    submit() {
        if (this.form.valid) {
            const username = this.form.get('username')?.value
            const password = this.form.get('password')?.value

            this.authService.login(username, password).subscribe({
                next: (data) => {
                    if (data && data.access_token) {
                        this.authService.getUserData()
                            .subscribe(() => {
                                this.router.navigate(["/"]);
                            })
                    }
                },
                error: ({ error }) => {
                    this.error = error.errors.join('') || '';
                }
            });
        }
    }
}
