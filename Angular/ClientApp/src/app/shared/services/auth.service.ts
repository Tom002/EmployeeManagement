import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { LoginResponseDto } from 'src/app/api/app.generated';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  currentUser: LoginResponseDto = {};

  constructor() {}

  setUser(user: LoginResponseDto) {
    this.currentUser = user;
    localStorage.setItem('access_token', user.token!);
  }

  getToken() {
    return localStorage.getItem('access_token');
  }

  doLogout() {
    localStorage.removeItem('access_token');
    this.currentUser = {};
  }

  get isLoggedIn(): boolean {
    let authToken = localStorage.getItem('access_token');
    return authToken !== null ? true : false;
  }
}
