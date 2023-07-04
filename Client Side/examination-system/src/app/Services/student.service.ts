import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../Environment/environment';
import { TokenService } from './token.service';
import { Student } from '../Interfaces/All';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  constructor(private http: HttpClient, private tokenService: TokenService) {}
  base = environment.contentful.student;
  private options = {
    observe: 'response' as const,
    headers: new HttpHeaders().set('Content-Type', 'application/json'),
  };
  GetAllStudents() {
    return this.http.get(this.base, this.options);
  }
  GetStudentById(id: any) {
    return this.http.get<any>(`${this.base}/${id}`, this.options);
  }
  AddStudent(student: any) {
    return this.http.post(this.base, student, this.options);
  }
  EditStudent(id: any, student: any) {
    return this.http.put(`${this.base}/${id}`, student, this.options);
  }
  DeleteStudent(id: any) {
    return this.http.delete(`${this.base}/${id}`, this.options);
  }

  isNotStudent(): boolean {
    return (
      this.tokenService.GetRole() != 'Student' ||
      this.tokenService.GetRole() == null ||
      this.tokenService.GetToken() == null
    );
  }
}
