import { environment } from './../../Environment/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { TokenService } from 'src/app/Services/token.service';

@Component({
  selector: 'app-exam',
  templateUrl: './exam.component.html',
  styleUrls: ['./exam.component.css']
})
export class ExamComponent {
  // private options = {
  //   observe: 'response' as const,
  //   headers: new HttpHeaders()
  //       .set("Content-Type", "application/json")
        
  // };
  constructor(private http:HttpClient,private TokenService:TokenService){}
  ngOnInit(): void {
    // this.http.get(environment.contentful.exam+"/4").subscribe({
    //   next: (v) => {
    //     console.log(v)
    //   },
    //   error: (e) => console.error(e),
    //   complete: () => console.info('Success'),
    // });
    this.http.get(environment.contentful.exam+"/4", { headers: this.setheader }).subscribe({
      next: (v) => {
        console.log(v)
      },
      error: (e) => console.error(e),
      complete: () => console.info('Success'),
    });


  }
   
  get setheader(){
      const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.TokenService.GetToken()}`
    });
    return headers;
  }
      
    
}
