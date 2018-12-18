import { AlertifyService } from './../../_services/alertify.service';
import { AuthService } from './../../_services/auth.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Course } from 'src/app/_models/course';
import { CourseService } from 'src/app/_services/course.service';
import { ActivatedRoute } from '@angular/router';
import { NgForm, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig, BsLocaleService, defineLocale, plLocale } from 'ngx-bootstrap';
import { FileUploaderService } from './../../_services/fileUploader.service';
import { environment } from 'src/environments/environment';
defineLocale('pl', plLocale);

@Component({
  selector: 'app-instructor-course-management',
  templateUrl: './instructor-course-management.component.html',
  styleUrls: ['./instructor-course-management.component.css']
})
export class InstructorCourseManagementComponent implements OnInit {
  course: Course;
  @ViewChild('addModuleForm') addModuleForm: NgForm;
  model: any = {};
  bsConfig: Partial<BsDatepickerConfig>;
  baseStaticFilesUrl = 'http://localhost:5000/';

  constructor(private courseService: CourseService, private authService: AuthService, private route: ActivatedRoute,
    private alertify: AlertifyService, private bsLocale: BsLocaleService, private fileUploader: FileUploaderService) { }

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-default'
    };
    this.bsLocale.use('pl');
    this.getCourse();
  }

  upload(files: any, moduleId: number) {
    if (files.length > 0) {
      this.fileUploader.upload(files, moduleId).subscribe(() => {
        this.alertify.success('Plik został wysłany.');
      }, error => {
        this.alertify.error('Nie udało się wysłać pliku.');
      });
    } else {
      this.alertify.error('Nie wybrałeś pliku.');
    }
  }

  addModule() {
    if (this.model.name && this.model.description) {
      this.courseService.addModule(this.course.id, this.model).subscribe((course: Course) => {
        this.course = course;
        this.alertify.success('Moduł "' + this.model.name + '" został dodany do kursu.');
        this.addModuleForm.reset();
      }, error => {
        this.alertify.error(error);
      });
    } else {
      this.alertify.error('Wypełnij wszystkie pola.');
    }
  }

  getCourse() {
    this.courseService.getCourse(+this.route.snapshot.params['id']).subscribe((course: Course) => {
      if (course !== null && course.instructor.id === +this.authService.decodedToken.nameid) {
        this.course = course;
      } else {
        this.alertify.error('Nie prowadzisz tego kursu.');
      }
    }, error => {
      this.alertify.error(error);
    });
  }
}