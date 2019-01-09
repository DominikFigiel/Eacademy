import { Assignment } from './../_models/assignment';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from './auth.service';
import { CourseStudents } from './../_models/courseStudents';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course } from '../_models/course';
import { Module } from '../_models/module';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  baseUrl = environment.apiUrl;
  courseStudents: CourseStudents;

  constructor(private http: HttpClient, private authService: AuthService, private alertify: AlertifyService) { }

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(this.baseUrl + 'courses/');
  }

  getCourse(id): Observable<Course> {
    return this.http.get<Course>(this.baseUrl + 'courses/' + id);
  }

  addCourseStudent(courseId: number) {
    return this.http.post(this.baseUrl + 'courses/' + courseId + '/addCourseStudent', {});
    /*
      return this.http.post(this.baseUrl + 'courses/' + courseId + '/addCourseStudent', {})
        .pipe(
          map((response: any) => {
            const course = response;
            if (course) {
              // console.log(course);
              // this.alertify.success('Zostałeś zapisany na kurs.');
            }
          })
        );
    */
  }

  deleteCourseStudent(courseId: number) {
    return this.http.delete(
      this.baseUrl + 'courses/' + courseId + '/deleteCourseStudent', {}
    );
  }

  addModule(courseId: number, model: any) {
    return this.http.post(
      this.baseUrl + 'courses/' + courseId + '/addModule/', model
    );
  }

  getAssignmentsByModule(moduleId: number) {
    return this.http.get<Assignment[]>(this.baseUrl + 'courses/module/getAssignmentsByModule/' + moduleId);
  }

  setGradeOfAnAssignment(assignment: any) {
    return this.http.put(this.baseUrl + 'courses/module/setGrade/' + assignment.id, assignment);
  }

  getAssignmentsByStudent(studentId: number) {
    return this.http.get<Assignment[]>(this.baseUrl + 'courses/module/getAssignments/' + studentId);
  }

  getAssignmentByStudent(studentId: number, moduleId: number) {
    return this.http.get(this.baseUrl + 'courses/module/getAssignment/' + studentId + '/' + moduleId);
  }

  getModule(id: number) {
    return this.http.get(this.baseUrl + 'courses/module/' + id);
  }

  addModuleAssignment(mod: any) {
    return this.http.put(this.baseUrl + 'courses/module/addAssignment/' + mod.id, mod);
  }

  addCourse(model: any) {
    return this.http.post(
      this.baseUrl + 'courses/' + 'addCourse', model
    );
  }

  getCoursesForList() {
    return this.http.get(this.baseUrl + 'admin/coursesForList');
  }

  getCoursesByInstructor(instructorId: number) {
    return this.http.get(this.baseUrl + 'courses/coursesByInstructor/' + instructorId);
  }

  deleteCourse(courseId: number) {
    return this.http.delete(
      this.baseUrl + 'courses/' + courseId + '/deleteCourse', {}
    );
  }

}
