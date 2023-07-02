import { TokenService } from './../../Services/token.service';
import { LoginService } from './../../Services/login.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private LoginService:LoginService,private token:TokenService){}
login(){
  this.LoginService.logIn("","").subscribe({
    next:(data) => {
      console.log(data.token);
      this.token.SaveToken(data.token);
    }
  })
}
}
