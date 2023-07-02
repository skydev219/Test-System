import { Injectable } from '@angular/core';
// import{CookieService} from'ngx-cookie-service';

// const TokenKey:string = "Token";
@Injectable({
  providedIn: 'root'
})

export class TokenService {

  constructor() { }
  GetToken(){
    return localStorage.getItem("Token");
    // return this.cookieServiece.get("Token"); 
  }
  SaveToken(token:string){
    return localStorage.setItem("Token",token);
    // this.cookieServiece.set("Token",token);
    //add isloggedin from login service
  }
  ClearToken(){
    localStorage.removeItem("Token");
    // this.cookieServiece.delete("Token");
  }
}
