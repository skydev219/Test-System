import { Injectable } from '@angular/core';
import{CookieService} from'ngx-cookie-service';

// const TokenKey:string = "Token";
@Injectable({
  providedIn: 'root'
})

export class TokenService {

  constructor(private cookieServiece:CookieService) { }
  GetToken(){
    return this.cookieServiece.get("Token"); 
  }
  SaveToken(token:string){
    this.cookieServiece.set("Token",token);
    //add isloggedin from login service
  }
  ClearToken(){
    this.cookieServiece.delete("Token");
  }
}
