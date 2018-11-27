import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CourseService } from '../_services/course.service';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
};

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {
  courses: any;

  constructor(private http: HttpClient, private courseService: CourseService, private authService: AuthService,
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.getCourses();
  }

  getCourses() {
    this.http.get('http://localhost:5000/api/courses', httpOptions).subscribe(response => {
        this.courses = response;
    }, error => {
      console.log(error);
    });
  }

  addCourseStudent(courseId) {
    // this.courseService.addCourseStudent(courseId).subscribe();
    this.courseService.addCourseStudent(courseId).subscribe(next => {
      this.alertify.success('Zapisałeś się na kurs.');
    }, error => {
      this.alertify.error('Jesteś już zapisany na ten kurs.');
    });
  }

}
