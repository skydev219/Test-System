import { Component, OnInit } from '@angular/core';
import { StudentService } from '../../../Services/student.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css'],
})
export class AdminLoginComponent implements OnInit {
  constructor(private studentService: StudentService, private router: Router) {}
  ngOnInit(): void {
    if (!this.studentService.isNotStudent()) {
      this.router.navigate(['/home']);
    }
  }
}
