import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../Environment/environment';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  constructor(private http: HttpClient) {}
  base = environment.contentful.student;
  private options = {
    observe: 'response' as const,
    headers: new HttpHeaders().set('Content-Type', 'application/json'),
  };
  GetAllStudents() {
    return this.http.get(this.base, this.options);
  }
  GetStudentById(id: any) {
    return this.http.get(`${this.base}/${id}`, this.options);
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
}
