import { Component } from '@angular/core';
import { FormControl, FormGroup,Validators } from '@angular/forms';

@Component({
  selector: 'app-studentlogin',
  templateUrl: './studentlogin.component.html',
  styleUrls: ['./studentlogin.component.css']
})
export class StudentloginComponent {
loginForm = new FormGroup({
  Username : new FormControl('',[Validators.required]),
  Password : new FormControl('',[Validators.required])
});
get GetUserName(){
  return this.loginForm.controls['Username'];
}
get GetPassword(){
  return this.loginForm.controls['Password'];
}


}
