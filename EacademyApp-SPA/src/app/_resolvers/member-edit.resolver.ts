import { Observable, of } from 'rxjs';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Injectable } from '@angular/core';
import { StudentService } from '../_services/student.service';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Student } from '../_models/student';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class StudentEditResolver implements Resolve<Student> {
    constructor(private studentService: StudentService, private authService: AuthService,
        private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Student> {
        return this.studentService.getStudent(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.alertify.error('Podczas odczytu danych wystąpił problem.');
                this.router.navigate(['/students']);
                return of(null);
            })
        );
    }
}
