import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
 @Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
   constructor(private authService: AuthService) { }
   ngOnInit() {
  }
   studentRegister() {
    this.authService.studentRegister(this.model).subscribe(() => {
      console.log('Success');
    }, error => {
      console.log('Error');
    });
  }
   cancel() {
    this.cancelRegister.emit(false);
    console.log('cancelled');
  }
 }