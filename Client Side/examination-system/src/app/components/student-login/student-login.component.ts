import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-student-login',
  templateUrl: './student-login.component.html',
  styleUrls: ['./student-login.component.css'],
})
export class StudentLoginComponent {
  loginForm = new FormGroup({
    Username: new FormControl('', [Validators.required]),
    Password: new FormControl('', [Validators.required]),
  });
  get GetUserName() {
    return this.loginForm.controls['Username'];
  }
  get GetPassword() {
    return this.loginForm.controls['Password'];
  }
}
