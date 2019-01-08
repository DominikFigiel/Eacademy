import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FileUploaderService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private authService: AuthService) { }

  upload(files: any, moduleId: number) {
    if (files.length === 0) {
      return;
    }

    const formData = new FormData();

    for (const file of files) {
      const filename = 'module_' + moduleId + '.zip';
      formData.append(filename, file, filename);
    }

    return this.http.post(this.baseUrl + 'courses/module/addFile/' + moduleId, formData).pipe(
      map((response: any) => {})
    );
  }

  uploadAssignment(files: any, moduleId: number) {
    if (files.length === 0) {
      return;
    }

    const formData = new FormData();

    const studentId = this.authService.decodedToken.nameid;

    for (const file of files) {
      const filename = 'module_' + moduleId + '_student_' + studentId + '.zip';
      formData.append(filename, file, filename);
    }

    return this.http.post(this.baseUrl + 'courses/module/sendAssignment/' + moduleId + '/' + studentId, formData).pipe(
      map((response: any) => {})
    );
  }

}
