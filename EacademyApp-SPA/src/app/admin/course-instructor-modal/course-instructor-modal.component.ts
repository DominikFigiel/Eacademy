import { Module } from './../../_models/module';
import { AlertifyService } from './../../_services/alertify.service';
import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Student } from 'src/app/_models/student';
import { BsModalRef } from 'ngx-bootstrap';
import { Course } from 'src/app/_models/course';

@Component({
  selector: 'app-course-instructor-modal',
  templateUrl: './course-instructor-modal.component.html',
  styleUrls: ['./course-instructor-modal.component.css']
})
export class CourseInstructorModalComponent implements OnInit {
  @Output() updateSelectedCourse = new EventEmitter;
  course: Course;
  instructors: Student[];
  instructorId: number;

  constructor(public bsModalRef: BsModalRef, private alertify: AlertifyService) {}

  ngOnInit() {
  }

  update() {
    if (this.instructorId) {
      this.updateSelectedCourse.emit(this.instructorId);
    } else {
      this.alertify.message('Nie wybrano instruktora.');
    }
    this.bsModalRef.hide();
  }

}
