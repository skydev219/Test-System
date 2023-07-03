import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  
  constructor(private http:HttpClient) { }
  base=environment.contentful.question;

  private options = {
    observe: 'response' as const,
    headers: new HttpHeaders()
        .set("Content-Type", "application/json")
        
  };
  GetAllQuestions(){
    return this.http.get(this.base,this.options);
  }
  GetQuestionById(id:any){
    return this.http.get(`${this.base}/${id}`,this.options);
  }
  AddQuestion(question:any){
    return this.http.post(this.base,question,this.options);
  }
  EditQuestion(id:any,question:any){
    return this.http.put(`${this.base}/${id}`,question,this.options);
  }
  DeleteQuestion(id:any){
    return this.http.delete(`${this.base}/${id}`,this.options);
  }
}
