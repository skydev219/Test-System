import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-student-register',
  templateUrl: './student-register.component.html',
  styleUrls: ['./student-register.component.css'],
})
export class StudentRegisterComponent {
  registerForm = new FormGroup({
    Name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    UserName: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
    ]),
    Pass: new FormControl('', [Validators.required, Validators.minLength(3)]),
  });
  get GetName() {
    return this.registerForm.controls['Name'];
  }
  get GetUserName() {
    return this.registerForm.controls['UserName'];
  }
  get GetPassword() {
    return this.registerForm.controls['Pass'];
  }
}
