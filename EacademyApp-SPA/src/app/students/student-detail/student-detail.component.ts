import { StudentService } from './../../_services/student.service';
import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Student } from 'src/app/_models/student';

@Component({
  selector: 'app-student-detail',
  templateUrl: './student-detail.component.html',
  styleUrls: ['./student-detail.component.css']
})
export class StudentDetailComponent implements OnInit {
  student: Student;

  constructor(private studentService: StudentService, private alertify: AlertifyService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadStudent();
  }

  // students/4
  loadStudent() {
    // znak + sprawia ze params['id'] (string) jest zamieniany na liczbe
    this.studentService.getStudent(+this.route.snapshot.params['id']).subscribe((student: Student) => {
      this.student = student;
    }, error => {
      this.alertify.error(error);
    });
  }

}
