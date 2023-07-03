import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../Environment/environment';

@Injectable({
  providedIn: 'root',
})
export class GradeService {
  constructor(private http: HttpClient) {}
  base = environment.contentful;

  private options = {
    observe: 'response' as const,
    headers: new HttpHeaders().set('Content-Type', 'application/json'),
  };
  GetAllGrades() {
    return this.http.get(this.base.grade, this.options);
  }
  GetGradetById(st_id: any, ex_id: any) {
    return this.http.get(`${this.base.grade}/${st_id},${ex_id}`, this.options);
  }
  GetGradeByStudent(st_id: any) {
    return this.http.get(
      `${this.base.getGradeByStudent}/${st_id}`,
      this.options
    );
  }
  GetGradeByExam(ex_id: any) {
    return this.http.get(
      `${this.base.getGradeByStudent}/${ex_id}`,
      this.options
    );
  }
  AddGrade(grade: any) {
    return this.http.post(this.base.grade, grade, this.options);
  }
  EditStudent(grade: any) {
    return this.http.put(this.base.grade, grade, this.options);
  }
  DeleteStudent(st_id: any, ex_id: any) {
    return this.http.delete(`${this.base}/${st_id},${ex_id}`, this.options);
  }
}
