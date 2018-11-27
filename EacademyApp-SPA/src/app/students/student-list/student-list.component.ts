import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../_services/alertify.service';
import { StudentService } from '../../_services/student.service';
import { Student } from 'src/app/_models/student';
 @Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {
  students: Student[];
   constructor(private studentService: StudentService, private alertify: AlertifyService) { }
   ngOnInit() {
    this.loadStudents();
  }
   loadStudents() {
    this.studentService.getStudents().subscribe((students: Student[]) => {
      this.students = students;
    }, error => {
      this.alertify.error(error);
    });
  }
 }
 