import { TokenService } from 'src/app/Services/token.service';
import { LoginService } from './../../../Services/login.service';
import { Component, OnInit } from '@angular/core';
import { StudentService } from '../../../Services/student.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css'],
})
export class AdminLoginComponent implements OnInit {
  constructor(private studentService: StudentService, private router: Router,private LoginService:LoginService,private TokenService:TokenService) {}
  ngOnInit(): void {
    if (!this.studentService.isNotStudent()) {
      this.router.navigate(['/home']);
    }
  }
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
    console.log(this.loginForm.value);

    e.preventDefault();
    if (this.loginForm.status === 'VALID') {
      this.LoginService.loginAdmin(
        this.GetUserName?.value,
        this.GetPassword?.value
      ).subscribe({
        
        next: (response: any) => {
          console.log(this.loginForm.value);
          console.log(response);
          if (response.ok) {
            let token = response.body.response.token;
            let role = response.body.response.role;
            this.TokenService.SaveToken(1,token, role, this.GetUserName?.value?this.GetUserName?.value:'');
            this.router.navigate(['/admin/home']);
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
