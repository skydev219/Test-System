import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StudentService } from 'src/app/Services/student.service';

@Component({
  selector: 'app-student-register',
  templateUrl: './student-register.component.html',
  styleUrls: ['./student-register.component.css'],
})
export class StudentRegisterComponent {
  constructor(private StudentService: StudentService, private Router: Router) {}
  registerForm = new FormGroup({
    ID: new FormControl(0),
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
  addStudent(e: any) {
    e.preventDefault();
    if (this.registerForm.status === 'VALID') {
      console.log(this.registerForm.value);
      this.StudentService.AddStudent(this.registerForm.value).subscribe({
        next: (res) => {
          console.log(res);
          if (res.ok) {
            this.Router.navigate(['/home']);
          }
        },
        error: (e) => {
          this.showAlert();
          console.error(e);
        },
        complete: () => console.info('Success'),
      });
    } else {
      this.showAlert();
    }
  }
  showAlert() {
    $('#myAlert').addClass('show');
  }
  closeAlert() {
    $('#myAlert').removeClass('show');
  }
}
