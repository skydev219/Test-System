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
  editexam_name:string='';
  editexam_id:any;
  addForm = new FormGroup({
    examname:new FormControl('',[Validators.required,Validators.minLength(3),Validators.maxLength(50)])
  });
  editForm = new FormGroup({
    editname:new FormControl('',[Validators.required,Validators.minLength(3),Validators.maxLength(50)])
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
  get getEditName(){
    return this.editForm.controls['editname'];
  }
  add(){
    if (this.addForm.status === 'VALID') {
      let exam:Exam = {id:0,name:this.getExamName.value ??''}
      this.examService.AddExam(exam).subscribe({
        
        next:(response)=>{
          console.log(response);
          this.exams.push(exam);
          this.getExamName.setValue('');
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
  delete(id:any){
    if (confirm(`Are you sure to delete`)) {
      this.examService.DeleteExam(id).subscribe({
        next:(response)=>{
         this.exams= this.exams.filter( e => e.id != id);
         console.log(response);
        },
        error: (e) => {
          // this.showAlert();
          console.log(e);
        },
        complete: () => console.info('Success')
      })
    }

  }
  edit(name:any,id:any){
    this.getEditName.setValue(name);
    this.editexam_name=name;
    this.editexam_id=id;
  }
  editexam(){
    if (this.editForm.status === 'VALID') {
      if (this.editexam_id) {
        if (this.editexam_name === this.getEditName.value) {
          console.log('No Changes')
          return
        }
        let exam:Exam = {id:this.editexam_id,name:this.getEditName.value ??''}
        this.examService.EditExam(this.editexam_id,exam).subscribe({
          next:(response)=>{
            this.exams= this.exams.filter( e => e.id != this.editexam_id);
            this.exams.push(exam);
            this.getEditName.setValue('');
           console.log(response);
          },
          error: (e) => {
            // this.showAlert();
            console.log(e);
          },
          complete: () => console.info('Success')
        })
      }
    }

  }
}
