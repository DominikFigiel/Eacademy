import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { Student } from '../_models/student';
import { Course } from '../_models/course';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
    return this.http.get(this.baseUrl + 'admin/usersWithRoles');
  }

  updateUserRoles(user: Student, roles: {}) {
    return this.http.post(this.baseUrl + 'admin/editRoles/' + user.username, roles);
  }

  updateCourseInstructor(course: Course, instructorId: number) {
    return this.http.put(this.baseUrl + 'courses/' + course.id + '/setInstructor/' + instructorId, course);
  }

}
