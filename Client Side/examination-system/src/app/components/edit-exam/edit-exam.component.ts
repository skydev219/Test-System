import { AnswerService } from './../../Services/answer.service';
import { QuestionService } from './../../Services/question.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Answer, Exam, Question } from 'src/app/Interfaces/All';
import { ExamService } from 'src/app/Services/exam.service';
import { StudentService } from 'src/app/Services/student.service';

@Component({
  selector: 'app-edit-exam',
  templateUrl: './edit-exam.component.html',
  styleUrls: ['./edit-exam.component.css']
})
export class EditExamComponent implements OnInit {
  examId:any;
  examname:any;
  exam?:Exam;
  questions:Question[]=[];
  edit_question?:Question;
  constructor(private route:ActivatedRoute, 
    private examservice:ExamService, 
    private router:Router,
    private studentService:StudentService,
    private questionService:QuestionService,
    private answerService:AnswerService
    ){
    this.route.params.subscribe((params) => {
      this.examId = params['id'];
    }); 
  }
  ngOnInit(): void {
    if (!this.studentService.isNotStudent()) {
      this.router.navigate(['home']);
    } else {
      this.examservice.GetExamById(this.examId).subscribe({
        next: (v: any) => {
          this.exam = v.body;
          this.examname=this.exam?.name;
          this.questions=this.exam?.questions ??[];
          console.log(this.questions)
        },
        error: (e) => console.error(e),
        complete: () => console.info('Success'),
      });
    }
  }
  addForm = new FormGroup({
    question:new FormControl('',[Validators.required,Validators.minLength(3),Validators.maxLength(50)]),
    answer:new FormControl('',[Validators.required,Validators.minLength(3),Validators.maxLength(50)])
  });
  editForm = new FormGroup({
    question:new FormControl('',[Validators.required,Validators.minLength(3),Validators.maxLength(50)]),
    answer:new FormControl('',[Validators.required,Validators.minLength(3),Validators.maxLength(50)])
  });
  get getQuestion(){
    return this.addForm.controls['question'];
  }
  get getAnswer(){
    return this.addForm.controls['answer'];
  }
  get getEditQuestion(){
    return this.editForm.controls['question'];
  }
  get getEditAnswer(){
    return this.editForm.controls['answer'];
  }
  add(){
    debugger
    if (this.addForm.status === 'VALID') {
      let answer:Answer = {id:0,name:this.getAnswer.value ?? '',q_ID:0,q_IDNavigation:''}
      let question:Question = {id:0,name:this.getQuestion.value ??'',exam_ID:this.examId,exam:'',answer:answer}
      this.questionService.AddQuestion(question).subscribe({
        
        next:(response:any)=>{
          // console.log(response);
          // answer.q_ID=response.body.id;
          console.log(answer);
          console.log(response);
          this.questions.push(question);
          this.getQuestion.setValue('');
          this.getAnswer.setValue('');
        },
        error: (e) => {
          // this.showAlert();
          console.log(e);
        },
        complete: () => console.info('Success'),
      });
    }
  }
  edit(question:Question){
    this.getEditQuestion.setValue(question.name);
    this.getEditAnswer.setValue(question.answer.name);
    this.edit_question = question;
  }
  editquestion(){
    if (this.editForm.status === 'VALID') {
      if (this.edit_question) {
        if (this.edit_question.name === this.getEditQuestion.value && this.edit_question.answer.name === this.getEditAnswer.value) {
          console.log('No Changes')
          return
        }
        let answer:Answer = {id:this.edit_question.answer.id,name:this.getEditAnswer.value ?? '',q_ID:this.edit_question.answer.q_ID,q_IDNavigation:''}
        let question:Question = {id:this.edit_question.id,name:this.getEditQuestion.value ??'',exam_ID:this.examId,exam:'',answer:answer}
        this.questionService.EditQuestion(this.edit_question.id,question).subscribe({
          next:(response)=>{
            this.questions= this.questions.filter( e => e.id != this.edit_question?.id);
            this.questions.push(question);
            this.getEditQuestion.setValue('');
            this.getEditAnswer.setValue('');
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
  delete(id:any){
    if (confirm(`Are you sure to delete`)) {
      this.questionService.DeleteQuestion(id).subscribe({
        next:(response)=>{
         this.questions= this.questions.filter( e => e.id != id);
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
