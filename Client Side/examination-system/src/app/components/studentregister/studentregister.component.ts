import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component } from '@angular/core';

@Component({
  selector: 'app-studentregister',
  templateUrl: './studentregister.component.html',
  styleUrls: ['./studentregister.component.css']
})
export class StudentregisterComponent {
registerForm = new FormGroup({
  Name: new FormControl('',[Validators.required,Validators.minLength(3)]),
  UserName: new FormControl('',[Validators.required,Validators.minLength(5)]),
  Pass:new FormControl('',[Validators.required,Validators.minLength(3)])
});
get GetName(){
  return this.registerForm.controls['Name'];
}
get GetUserName(){
  return this.registerForm.controls['UserName'];
}
get GetPassword(){
  return this.registerForm.controls['Pass'];
}











}
