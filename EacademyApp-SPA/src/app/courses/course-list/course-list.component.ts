import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from '../../_services/auth.service';
import { CourseService } from '../../_services/course.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap';

const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
};

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {
  courses: any;
  currentCoursesPageContent: any;
  itemsPerPage: number;
  previousPageText: string;
  nextPageText: string;

  constructor(private http: HttpClient, private courseService: CourseService, private authService: AuthService,
      private alertify: AlertifyService) { }

  ngOnInit() {
    this.getCourses();
    this.itemsPerPage = 4;
    this.previousPageText = 'Poprzednia';
    this.nextPageText = 'Następna';
  }

  pageChanged(event: PageChangedEvent): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.currentCoursesPageContent = this.courses.slice(startItem, endItem);
  }

  getCourses() {
    this.http.get('http://localhost:5000/api/courses', httpOptions).subscribe(response => {
        this.courses = response;
        this.currentCoursesPageContent = this.courses.slice(0, 4);
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
