import { CourseService } from 'src/app/_services/course.service';
import { Component, OnInit, Input } from '@angular/core';
import { Student } from 'src/app/_models/student';
import { Course } from 'src/app/_models/course';

@Component({
  selector: 'app-instructor-course-list',
  templateUrl: './instructor-course-list.component.html',
  styleUrls: ['./instructor-course-list.component.css']
})
export class InstructorCourseListComponent implements OnInit {
  @Input() student: Student;
  courses: Course[];

  constructor(private courseService: CourseService) { }

  ngOnInit() {
    this.getCourses();
  }

  getCourses() {
    this.courseService.getCoursesByInstructor(this.student.id).subscribe((courses: Course[]) => {
      this.courses = courses;
    }, error => {
      console.log(error);
    });
  }

}
