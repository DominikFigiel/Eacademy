import { AuthService } from './../../_services/auth.service';
import { StudentService } from './../../_services/student.service';
import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Student } from 'src/app/_models/student';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-student-edit',
  templateUrl: './student-edit.component.html',
  styleUrls: ['./student-edit.component.css']
})
export class StudentEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  student: Student;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute, private alertify: AlertifyService,
    private studentService: StudentService, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.student = data['student'];
    });
  }

  updateUser() {
    this.studentService.updateStudent(this.authService.decodedToken.nameid, this.student).subscribe(next => {
      this.alertify.success('Zmiany zostały zapisane.');
      this.editForm.reset(this.student);
    }, error => {
      this.alertify.error('Wystąpił błąd. Zmiany nie zostały zapisane.');
    });
  }

}