import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Student } from 'src/app/_models/student';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { StudentService } from 'src/app/_services/student.service';
import { AuthService } from 'src/app/_services/auth.service';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Course } from '../../_models/course';
import { PageChangedEvent } from 'ngx-bootstrap';

@Component({
  selector: 'app-user-course-list',
  templateUrl: './user-course-list.component.html',
  styleUrls: ['./user-course-list.component.css']
})

export class UserCourseListComponent implements OnInit {
  student: Student;
  currentCoursesPageContent: any;
  itemsPerPage: number;
  previousPageText: string;
  nextPageText: string;

  constructor(private http: HttpClientModule, private studentService: StudentService, private authService: AuthService,
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadCourses();
    this.itemsPerPage = 4;
    this.previousPageText = 'Poprzednia';
    this.nextPageText = 'Następna';
  }
  
  loadCourses() {
    this.studentService.getStudent(this.authService.decodedToken.nameid).subscribe((student: Student) => {
      this.student = student;
      this.currentCoursesPageContent = this.student.courseStudents.slice(0, this.itemsPerPage);
    }, error => {
      this.alertify.error(error);
    });
  }
}