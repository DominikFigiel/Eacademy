import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

constructor() { }

}
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from './auth.service';
import { CourseStudents } from './../_models/courseStudents';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Course } from '../_models/course';

@Injectable({
  providedIn: 'root'
})

export class CourseService {
  baseUrl = environment.apiUrl;
  courseStudents: CourseStudents;
  constructor(private http: HttpClient, private authService: AuthService,
    private alertify: AlertifyService) { }

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(this.baseUrl + 'courses/');
  }

  getCourse(id): Observable<Course> {
    return this.http.get<Course>(this.baseUrl + 'courses/' + id);
  }

  addCourseStudent(courseId: number) {
    const options = {
      params: new HttpParams()
        .set('id', courseId.toString())
    };
    return this.http.post('http://localhost:5000/api/courses/addCourseStudent', {}, options)
      .pipe(
        map((response: any) => {
          const course = response;
          if (course) {
            // console.log(course);
            // this.alertify.success('Zostałeś zapisany na kurs.');
          }
        })
    );
  }
}