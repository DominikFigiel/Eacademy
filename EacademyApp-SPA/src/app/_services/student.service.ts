import { environment } from './../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../_models/student';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.baseUrl + 'students/');
  }

  getStudent(id): Observable<Student> {
    return this.http.get<Student>(this.baseUrl + 'students/' + id);
  }

  updateStudent(id: number, student: Student) {
    return this.http.put(this.baseUrl + 'students/' + id, student);
  }
}
