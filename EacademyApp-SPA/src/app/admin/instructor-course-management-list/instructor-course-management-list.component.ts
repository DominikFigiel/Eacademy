import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/_models/course';
import { CourseService } from 'src/app/_services/course.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-instructor-course-management-list',
  templateUrl: './instructor-course-management-list.component.html',
  styleUrls: ['./instructor-course-management-list.component.css']
})

export class InstructorCourseManagementListComponent implements OnInit {
  courses: Course[];

  constructor(private courseService: CourseService, private authService: AuthService) { }

  ngOnInit() {
    this.getCourses();
  }

  getCourses() {
    this.courseService.getCoursesByInstructor(this.authService.decodedToken.nameid).subscribe((courses: Course[]) => {
      this.courses = courses;
    }, error => {
      console.log(error);
    });
  }
}