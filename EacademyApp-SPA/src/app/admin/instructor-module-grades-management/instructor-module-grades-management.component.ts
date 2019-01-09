import { AlertifyService } from 'src/app/_services/alertify.service';
import { CourseService } from 'src/app/_services/course.service';
import { Assignment } from 'src/app/_models/assignment';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Module } from 'src/app/_models/module';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-instructor-module-grades-management',
  templateUrl: './instructor-module-grades-management.component.html',
  styleUrls: ['./instructor-module-grades-management.component.css']
})
export class InstructorModuleGradesManagementComponent implements OnInit {
  baseStaticFilesUrl = 'http://localhost:5000/';
  moduleId: number;
  module: Module;
  assignments: Assignment[];

  constructor(private route: ActivatedRoute, private courseService: CourseService,
    private alertify: AlertifyService, private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    // sprawdzenie czy wchodzi wlasciwy profesor
    this.moduleId = +this.route.snapshot.params['moduleId'];
    this.getModule();
    this.getAssignments();
    //
  }

  getAssignments() {
    this.courseService.getAssignmentsByModule(this.moduleId).subscribe((assignments: Assignment[]) => {
      this.assignments = assignments;
    }, error => {
      this.alertify.error(error);
    });
  }

  getModule() {
    const loggedUserId = +this.authService.decodedToken.nameid;

    this.courseService.getModule(this.moduleId).subscribe((mod: Module) => {
      if (mod === null) {
        this.alertify.error('Brak dostępu.');
        this.router.navigate(['/home']);
      } else if (loggedUserId === mod.course.instructor.id) {
        this.module = mod;
      } else {
        this.alertify.error('Tym kursem zarządza inny instruktor!');
        this.router.navigate(['/home']);
      }
    }, error => {
      this.alertify.error(error);
    });
  }

  setGrade(assignment: Assignment, grade: number) {
    assignment.grade = grade;
    if (grade) {
      this.courseService.setGradeOfAnAssignment(assignment).subscribe(() => {
        this.alertify.success('Ocena została zapisana.');
      }, error => {
        this.alertify.error(error);
      });
    } else {
      this.alertify.error('Nie wybrałeś oceny zadania.');
    }
  }

}