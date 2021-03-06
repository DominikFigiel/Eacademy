import { AlertifyService } from './../_services/alertify.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from './../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public authService: AuthService, private alertify: AlertifyService,
    private router: Router) { }

  ngOnInit() {
  }

  studentLogin() {
    this.authService.studentLogin(this.model).subscribe(next => {
      this.alertify.success('Zalogowano.');
    }, error => {
      this.alertify.error('Logowanie nieudane.');
    }, () => {
      this.router.navigate(['/welcome']);
    });
  }

  studentLoggedIn() {
    return this.authService.studentLoggedIn();
  }

  studentLogout() {
    localStorage.removeItem('token');
    this.alertify.message('Zostałeś wylogowany.');
    this.router.navigate(['/home']);
  }

}
