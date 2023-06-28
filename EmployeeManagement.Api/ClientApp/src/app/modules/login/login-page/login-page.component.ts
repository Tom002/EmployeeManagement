import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginClient } from 'src/app/api/app.generated';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
})
export class LoginPageComponent {
  loginForm!: FormGroup;
  loginValid: boolean = true;

  constructor(
    private readonly loginClient: LoginClient,
    private readonly fb: FormBuilder,
    private readonly auth: AuthService,
    private readonly router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.maxLength(100)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  submit() {
    if (this.loginForm.valid) {
      this.loginClient
        .login({
          username: this.loginForm.controls['username'].value,
          password: this.loginForm.controls['password'].value,
        })
        .subscribe(
          (result) => {
            this.loginValid = true;
            this.auth.setUser(result);
            this.router.navigate(['employees']);
          },
          (error) => {
            console.log(error);
            this.loginValid = false;
          }
        );
    }
  }

  public errorHandling = (control: string, error: string) => {
    return this.loginForm.controls[control].hasError(error);
  };
}
