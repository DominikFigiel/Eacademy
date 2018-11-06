import { Component, OnInit } from '@angular/core';
import { AuthService } from './../_services/auth.service';
 @Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
   constructor(private authService: AuthService) { }
   ngOnInit() {
  }
   studentLogin() {
    this.authService.studentLogin(this.model).subscribe(next => {
      console.log('Logged in successfully');
    }, error => {
      console.log('Failed to login');
    });
  }
   studentLoggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }
   studentLogout() {
    localStorage.removeItem('token');
    console.log('logged out');
  }
 }