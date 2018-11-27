import { environment } from './../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../_models/student';
 const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
};
 @Injectable({
  providedIn: 'root'
})
export class StudentService {
  baseUrl = environment.apiUrl;
   constructor(private http: HttpClient) { }
   getStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.baseUrl + 'students/', httpOptions);
  }
   getStudent(id): Observable<Student> {
    return this.http.get<Student>(this.baseUrl + 'students/' + id, httpOptions);
  }
 }