import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StudentService } from 'src/app/Services/student.service';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css'],
})
export class AdminHomeComponent implements OnInit {
  constructor(private studentService: StudentService, private router: Router) {}
  ngOnInit(): void {
    if (!this.studentService.isNotStudent()) {
      this.router.navigate(['/home']);
    }
  }
}
