import { Component, OnInit, Input } from '@angular/core';
import { Student } from 'src/app/_models/student';
import { Course } from 'src/app/_models/course';

@Component({
  selector: 'app-student-course-list',
  templateUrl: './student-course-list.component.html',
  styleUrls: ['./student-course-list.component.css']
})
export class StudentCourseListComponent implements OnInit {
  @Input() student: Student;

  constructor() { }

  ngOnInit() {
  }

}
