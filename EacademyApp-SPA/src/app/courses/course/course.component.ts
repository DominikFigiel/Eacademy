import { Module } from 'src/app/_models/module';
import { AuthService } from 'src/app/_services/auth.service';
import { FileUploaderService } from 'src/app/_services/fileUploader.service';
import { CourseService } from './../../_services/course.service';
import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/_models/course';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Assignment } from 'src/app/_models/assignment';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {
  course: Course;
  assignments: Assignment[];
  studentId: number;
  baseStaticFilesUrl = 'http://localhost:5000/';
  constructor(private courseService: CourseService, private route: ActivatedRoute, private authService: AuthService,
      private alertify: AlertifyService, private fileUploader: FileUploaderService) { }

  ngOnInit() {
    this.loadCourse();
    this.loadAssignments();
    this.studentId = this.authService.decodedToken.nameid;
  }

  loadCourse() {
    // znak + sprawia ze params['id'] (string) jest zamieniany na liczbe
    this.courseService.getCourse(+this.route.snapshot.params['id']).subscribe((course: Course) => {
      this.course = course;
    }, error => {
      this.alertify.error(error);
    });
  }

  loadAssignments() {
    const studentId = this.authService.decodedToken.nameid;
    this.courseService.getAssignmentsByStudent(+studentId).subscribe((assignments: Assignment[]) => {
      this.assignments = assignments;
    }, error => {
      this.alertify.error(error);
    });
  }

  assignmentSent(moduleId: number) {
    if (moduleId !== null) {
      if (this.assignments) {
        return this.assignments.some(e => e.moduleId === moduleId);
      } else {
        return false;
      }
    } else {
      return false;
    }
  }

  showGrade(moduleId: number, studentId: number) {
    if (moduleId === null) {
      return false;
    }
    if (moduleId && studentId && this.assignments) {
      if (this.assignments.find(a => a.moduleId === moduleId && a.studentId === +studentId)) {
        return true;
      } else {
        return false;
      }
    } else {
      return false;
    }
  }

  getIndexOfGrade(moduleId: number, studentId: number) {
    if (moduleId === null || this.assignments == null) {
      return -1;
    } else {
      return this.assignments.indexOf(this.assignments.find(a => a.moduleId === moduleId && a.studentId === +studentId));
    }
  }

  uploadAssignment(files: any, module: Module) {
    if (files.length > 0) {
      this.fileUploader.uploadAssignment(files, module.id).subscribe(() => {
        this.alertify.success('Plik został wysłany.');
      }, error => {
        this.alertify.error('Nie udało się wysłać pliku.');
      });
    } else {
      this.alertify.error('Nie wybrałeś pliku.');
    }

  }

}
