import { TokenService } from 'src/app/Services/token.service';
import { Grade } from 'src/app/Interfaces/All';
import { GradeService } from './../../../Services/grade.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentService } from 'src/app/Services/student.service';

@Component({
  selector: 'app-student-grades',
  templateUrl: './student-grades.component.html',
  styleUrls: ['./student-grades.component.css']
})
export class StudentGradesComponent implements OnInit {
  grades:Grade[]=[]
  examId?: number;
  examName?:string;

  constructor(private gradeService:GradeService,
    private tokenService:TokenService,
    private route:ActivatedRoute, 
    private studentservice:StudentService,
    private router:Router
    ){
    this.route.params.subscribe((params) => {
      this.examId = params['id'];
    });
  }
  ngOnInit(): void {
    if (!this.studentservice.isNotStudent()) {
      this.router.navigate(['home']);
    } 
    let id = this.tokenService.GetUserId();
    if (id) {
    this.gradeService.GetGradeByExam(this.examId).subscribe({
      next:(response:any) =>{
        this.grades=response.body;
        this.examName=response.body[0].exam;
        console.log(this.grades)

      },
      error: (e) => console.error(e),
      complete: () => console.info('Success'),
    });
      
    }
  }

}
