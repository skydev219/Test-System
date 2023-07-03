import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class AnswerService {

  constructor(private http:HttpClient) { }
  base=environment.contentful.answer;

  private options = {
    observe: 'response' as const,
    headers: new HttpHeaders()
        .set("Content-Type", "application/json")
        
  };
  GetAllAnswers(){
    return this.http.get(this.base,this.options);
  }
  GetAnswerById(id:any){
    return this.http.get(`${this.base}/${id}`,this.options);
  }
  AddAnswer(answer:any){
    return this.http.post(this.base,answer,this.options);
  }
  EditAnswer(id:any,answer:any){
    return this.http.put(`${this.base}/${id}`,answer,this.options);
  }
  DeleteAnswer(id:any){
    return this.http.delete(`${this.base}/${id}`,this.options);
  }

}
