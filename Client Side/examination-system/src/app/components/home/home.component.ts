import { TokenService } from './../../Services/token.service';
import { LoginService } from './../../Services/login.service';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/app/Environment/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit{
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
