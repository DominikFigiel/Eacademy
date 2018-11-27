import { Student } from './../../_models/student';
import { Component, OnInit, Input } from '@angular/core';
 @Component({
  selector: 'app-student-card',
  templateUrl: './student-card.component.html',
  styleUrls: ['./student-card.component.css']
})
export class StudentCardComponent implements OnInit {
  @Input() student: Student;
   constructor() { }
   ngOnInit() {
  }
 }
 