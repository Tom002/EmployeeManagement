import { Injectable } from '@angular/core';
import { LoginResponseDto } from 'src/app/api/app.generated';
import { JwtHelperService } from '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor() {}

  setUser(user: LoginResponseDto) {
    localStorage.setItem('access_token', user.token!);
  }

  getToken() {
    return localStorage.getItem('access_token');
  }

  doLogout() {
    localStorage.removeItem('access_token');
  }

  getUser() {
    const helper = new JwtHelperService();
    const decoded = helper.decodeToken(this.getToken()!);

    let result: LoginResponseDto = {
      id: +decoded['Id'],
      name: decoded['Name'],
    };
    return result;
  }

  get isLoggedIn(): boolean {
    let authToken = localStorage.getItem('access_token');
    return authToken !== null ? true : false;
  }
}
