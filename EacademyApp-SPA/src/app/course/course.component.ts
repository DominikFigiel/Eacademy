import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {
  courses: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getCourses();
  }

  getCourses() {
    this.http.get('http://localhost:5000/api/courses').subscribe(response => {
        this.courses = response;
    }, error => {
      console.log(error);
    });
  }

}