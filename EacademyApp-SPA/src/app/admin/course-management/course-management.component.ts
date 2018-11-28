import { AlertifyService } from 'src/app/_services/alertify.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Course } from 'src/app/_models/course';
import { CourseService } from 'src/app/_services/course.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-course-management',
  templateUrl: './course-management.component.html',
  styleUrls: ['./course-management.component.css']
})
export class CourseManagementComponent implements OnInit {
  @ViewChild('addCourseForm') addCourseForm: NgForm;
  model: any = {};
  coursesForList: Course[];

  constructor(private courseService: CourseService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.addCoursesForList();
  }

  addCourse() {
    if (this.model.name && this.model.description) {
      this.courseService.addCourse(this.model).subscribe((courses: Course[]) => {
        this.coursesForList = courses;
        this.alertify.success('Kurs "' + this.model.name + '" został dodany do bazy.');
        this.addCourseForm.reset();
      }, error => {
        this.alertify.error(error);
      });
    } else {
      this.alertify.error('Wypełnij wszystkie pola.');
    }
  }

  addCoursesForList() {
    this.courseService.getCoursesForList().subscribe((courses: Course[]) => {
      this.coursesForList = courses;
      console.log(this.coursesForList);
    }, error => {
      console.log(error);
    });
  }

  deleteCourseOnConfirm(courseId: number) {
    this.alertify.confirm('Usuwanie kursu', 'Chcesz usunąć ten kurs?', () => this.deleteCourse(courseId));
  }

  deleteCourse(courseId: number) {
    this.courseService.deleteCourse(courseId).subscribe((courses: Course[]) => {
      this.coursesForList = courses;
      this.alertify.message('Kurs został usunięty.');
    }, error => {
      this.alertify.error(error);
    });
  }

}
