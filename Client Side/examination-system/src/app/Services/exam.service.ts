import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../Environment/environment';

@Injectable({
  providedIn: 'root',
})
export class ExamService {
  constructor(private http: HttpClient) {}
  base = environment.contentful.exam;
  private options = {
    observe: 'response' as const,
    headers: new HttpHeaders().set('Content-Type', 'application/json'),
  };
  GetAllExams() {
    return this.http.get(this.base, this.options);
  }
  GetExamById(id: any) {
    return this.http.get(`${this.base}/${id}`, this.options);
  }
  AddExam(exam: any) {
    return this.http.post(this.base, exam, this.options);
  }
  EditExam(id: any, exam: any) {
    return this.http.put(`${this.base}/${id}`, exam, this.options);
  }
  DeleteExam(id: any) {
    return this.http.delete(`${this.base}/${id}`, this.options);
  }
}
