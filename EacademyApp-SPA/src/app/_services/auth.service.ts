import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
 @Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';
   constructor(private http: HttpClient) {}
   studentLogin(model: any) {
    return this.http.post(this.baseUrl + 'studentLogin', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user);
        }
      })
    );
  }
   studentRegister(model: any) {
    return this.http.post(this.baseUrl + 'studentRegister', model);
  }
   studentLoggedIn() {
    const token = localStorage.getItem('token');
    // return !this.jwtHelper.isTokenExpired(token);
  }
 }