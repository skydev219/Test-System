import { Exam } from './../../../Services/exam.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StudentService } from 'src/app/Services/student.service';
import { ExamService } from 'src/app/Services/exam.service';
import { FormControl, FormGroup, NgModel, Validators } from '@angular/forms';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css'],
})
export class AdminHomeComponent implements OnInit {
  exams: Exam[] = [];
  addForm = new FormGroup({
    examname:new FormControl('',[Validators.required,Validators.minLength(3)])
  });
  
  constructor(private studentService: StudentService, private router: Router,private examService: ExamService) {}
  ngOnInit(): void {
    if (!this.studentService.isNotStudent()) {
      this.router.navigate(['/home']);
    }
    this.examService.GetAllExams().subscribe({
      next: (v: any) => {
        this.exams = v.body;
        console.log(this.exams);
      },
      error: (e) => console.error(e),
      complete: () => console.info('Success'),
    });
  }
  get getExamName(){
    return this.addForm.controls['examname'];
  }
  add(){
    if (this.addForm.status === 'VALID') {
      this.examService.AddExam({id:2,name:this.getExamName.value}).subscribe({
        
        next:(response)=>{
          console.log(response);
          this.router.navigate([this.router.url])

        },
        error: (e) => {
          // this.showAlert();
          console.log(e);
        },
        complete: () => console.info('Success'),
      });
    }
  }
  
 
}
