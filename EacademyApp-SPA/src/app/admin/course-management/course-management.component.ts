import { Student } from './../../_models/student';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Course } from 'src/app/_models/course';
import { CourseService } from 'src/app/_services/course.service';
import { NgForm } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { CourseInstructorModalComponent } from '../course-instructor-modal/course-instructor-modal.component';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-course-management',
  templateUrl: './course-management.component.html',
  styleUrls: ['./course-management.component.css']
})
export class CourseManagementComponent implements OnInit {
  @ViewChild('addCourseForm') addCourseForm: NgForm;
  model: any = {};
  coursesForList: Course[];
  bsModalRef: BsModalRef;
  users: Student[];

  constructor(private courseService: CourseService, private adminSerivce: AdminService,
    private alertify: AlertifyService, private modalService: BsModalService) { }

  ngOnInit() {
    this.addCoursesForList();
    this.getInstructors();
    this.filterInstructors();
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
      // console.log(this.coursesForList);
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

  getInstructors() {
    this.adminSerivce.getUsersWithRoles().subscribe((users: Student[]) => {
      this.users = users;
    }, error => {
      console.log(error);
    });
  }

  filterInstructors() {
    if (this.users) {
      this.users = this.users.filter(u => u.isInstructor === true);
    }
  }

  editCourseInstructorModal(course: Course) {
    this.filterInstructors();
    const initialState = {
      course,
      instructors: this.users
    };
    this.bsModalRef = this.modalService.show(CourseInstructorModalComponent, {initialState});
    this.bsModalRef.content.updateSelectedCourse.subscribe((instructorId: number) => {
        this.adminSerivce.updateCourseInstructor(course, instructorId).subscribe((res: any) => {
          // course.instructor.name = res.instructor.name;
          // course.instructor.surname = res.instructor.surname;
          course.instructor = res.instructor;
          this.alertify.success('Instruktor został zmieniony.');
        }, error => {
          this.alertify.error(error);
        });

    });
  }

}
