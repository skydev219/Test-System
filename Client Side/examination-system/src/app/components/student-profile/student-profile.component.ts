import { Component, OnInit } from '@angular/core';
import { StudentService } from '../../Services/student.service';
import { Student } from '../../Interfaces/All';
import { TokenService } from '../../Services/token.service';

@Component({
  selector: 'app-student-profile',
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.css']
})
export class StudentProfileComponent implements OnInit{

  student: Student | undefined;
  studentname=localStorage.getItem('Username');
  constructor(private studentService: StudentService,private tokenService: TokenService) { }

  ngOnInit(): void {
    this.studentService.GetStudentById(this.tokenService.GetUserId()).subscribe({
      next: (v: any) => {
        this.student = v.body;
        console.log(this.student);
        this.student?.grades.forEach(element => {
          console.log(element.exam.name)
        });
      },
      error: (e) => console.error(e),
      complete: () => console.info('Success'),
    });

  }
}
