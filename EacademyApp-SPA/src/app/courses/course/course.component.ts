import { CourseService } from './../../_services/course.service';
import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/_models/course';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})

export class CourseComponent implements OnInit {
  course: Course;
  constructor(private courseService: CourseService, private route: ActivatedRoute,
      private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadCourse();
  }
  
  loadCourse() {
    // znak + sprawia ze params['id'] (string) jest zamieniany na liczbe
    this.courseService.getCourse(+this.route.snapshot.params['id']).subscribe((course: Course) => {
      this.course = course;
    }, error => {
      this.alertify.error(error);
    });
  }
}