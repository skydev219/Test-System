import { Router } from '@angular/router';
import { TokenService } from './../../Services/token.service';
import { LoginService } from './../../Services/login.service';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import * as $ from 'jquery';
import 'bootstrap';

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
    if (this.loginForm.status === 'VALID') {
      this.LoginService.logInStudent(
        this.GetUserName?.value,
        this.GetPassword?.value
      ).subscribe({
        next: (response: any) => {
          console.log(response);
          if (response.ok) {
            let token = response.body.response.token;
            let role = response.body.response.role;
            let username = response.body.response.student.username;
            let userid = response.body.response.student.id;
            this.LoginService.currentUser?.role;
            console.log();
            this.TokenService.SaveToken(userid,token, role, username);
            this.Router.navigate(['/home']);
          } else {
          }
        },
        error: (e) => {
          this.showAlert();
          console.log(e);
        },
        complete: () => console.info('Success'),
      });
    }
  }
  showAlert() {
    $('#myAlert').addClass('show');
  }
  closeAlert() {
    $('#myAlert').removeClass('show');
  }
}
