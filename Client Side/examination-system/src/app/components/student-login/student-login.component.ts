import { Router } from '@angular/router';
import { TokenService } from './../../Services/token.service';
import { LoginService } from './../../Services/login.service';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-student-login',
  templateUrl: './student-login.component.html',
  styleUrls: ['./student-login.component.css'],
})
export class StudentLoginComponent {
  constructor(
    private LoginService: LoginService,
    private TokenService: TokenService,
    private Router: Router
  ) {}

  loginForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });
  get GetUserName() {
    return this.loginForm.controls['username'];
  }
  get GetPassword() {
    return this.loginForm.controls['password'];
  }
  login(e: any) {
    e.preventDefault();
    debugger;
    if (this.loginForm.status === 'VALID') {
      this.LoginService.logInStudent(
        this.GetUserName?.value,
        this.GetPassword?.value
      ).subscribe({
        next: (res: any) => {
          console.log(res);
          if (res.ok) {
            let token = res.body.response.token;
            let role = res.body.response.role;
            console.log(role);
            console.log(token);
            this.LoginService.currentUser?.role;
            this.TokenService.SaveToken(token, role);
            // this.Router.navigate(['/home']);
          } else {
          }
        },
      });
    }
  }
}
