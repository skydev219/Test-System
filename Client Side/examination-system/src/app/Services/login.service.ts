import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { IUser } from '../Interfaces/iuser';
import { HttpClient } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { environment } from '../Environment/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(private router:Router,private http:HttpClient) { }
  currentUser:IUser|undefined;
  logIn(user:string|null,pass:string|null){
    this.currentUser={userName:user,password:pass};
    return this.http.get<{token:string}>(environment.contentful.token);
    // return this.http.post<IUser>( "real login api with diffrentiating between admin and student", this.currentUser)
    // .pipe(
    //   catchError( (err)=>{
    //     return throwError(err.message || "Server Issue")
    //   })
    // );
    //this.router.navigate(["/products"]);
    // this.TokenService.SaveToken();
  }
  logOut(){
    this.currentUser=undefined;
    this.router.navigate(["/login"]);
  }
  get isLoggedIn():boolean{
    if (this.currentUser?.userName && this.currentUser?.password) {
      return true;
    } else {
      return false;
    }
  }
}
