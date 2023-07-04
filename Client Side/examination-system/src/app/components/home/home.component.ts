import { TokenService } from './../../Services/token.service';
import { GradeService } from './../../Services/grade.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Output } from '@angular/core';
import { environment } from 'src/app/Environment/environment';
import { Exam, ExamService } from '../../Services/exam.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  exams: Exam[] = [];

  /**
   *Home Component Constructor
   */
  constructor(private http: HttpClient, private examService: ExamService, private gradeService:GradeService,private tokenService:TokenService,private router:Router) {}
  ngOnInit(): void {
    this.examService.GetAllExams().subscribe({
      next: (v: any) => {
        this.exams = v.body;
        console.log(this.exams);
      },
      error: (e) => console.error(e),
      complete: () => console.info('Success'),
    });
  }
  takeExam(exam_id:any){
    this.gradeService.GetGradetById(this.tokenService.GetUserId(),exam_id).subscribe({
      next:(response)=>{
        if (response.ok) {
          alert('you finished this exam');
        }
      },
      error:() =>{
        this.router.navigate(['/exams', exam_id]) 
      }
      
  })
  }
}
