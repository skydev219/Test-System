import { GradeService } from './../../Services/grade.service';
import { LoginService } from './../../Services/login.service';
import { Answer, Question, Grade } from './../../Interfaces/All';
import { environment } from './../../Environment/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TokenService } from 'src/app/Services/token.service';
import { ExamService } from '../../Services/exam.service';
import { Exam } from '../../Interfaces/All';
import { reduce } from 'rxjs';
import { StudentService } from 'src/app/Services/student.service';
import  Swal  from 'sweetalert2'

@Component({
  selector: 'app-exam',
  templateUrl: './exam.component.html',
  styleUrls: ['./exam.component.css'],
})
export class ExamComponent {
  counter:number = 0;
  correctAnswers:number = 0;
  Answers:Answer[] = [];
  AnswersUI:Answer[] = [];
  Questions?: Question[] =[];
  QuestionUI?:Question;
  SelectedAnswer?:Answer;
  examId?: number;
  exam: Exam | undefined;
  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private examService: ExamService,
    private studentService: StudentService,
    private GradeService:GradeService,
    private TokenService:TokenService
  ) {
    this.route.params.subscribe((params) => {
      this.examId = params['id'];
    });
  }
  ngOnInit(): void {

    if (this.studentService.isNotStudent()) {
      this.router.navigate(['/student/login']);
    } else {
      this.examService.GetExamById(this.examId).subscribe({
        next: (v: any) => {
          this.exam = v.body;
          this.createAnswersArray();
        },
        error: (e) => console.error(e),
        complete: () => console.info('Success'),
      });
    }

  }
  createAnswersArray(){
    if (this.exam) {
      this.Answers=[];
      this.AnswersUI=[];
      this.Questions=this.exam.questions;
      this.QuestionUI=this.Questions[this.counter];
      this.exam?.questions.forEach(q => {
      this.Answers.push(q?.answer);
      });
      this.AnswersUI.push(this.QuestionUI.answer);
      let ans_comparer = this.QuestionUI.answer;

      while (this.AnswersUI.length < 3) {
        let new_ans = this.Answers[Math.floor(Math.random() * this.Answers.length)];

        if (new_ans.id != ans_comparer.id && new_ans.id != this.AnswersUI[0].id) {
          this.AnswersUI.push(new_ans);
          ans_comparer = new_ans;
        }
      }
    }
  }
  submit(){
    this.counter++;
    if (this.counter == this.Questions?.length) {
      this.GradeService.AddGrade({st_id:this.TokenService.GetUserId(),exam_ID:this.exam?.id,grade1:this.correctAnswers}).subscribe({
        next: (response) => {
          let percentage = (this.correctAnswers / 10)*100
          Swal.fire({
            title: 'Congratulations',
            text: `You have finish Test\nGrade is ${percentage}%`,
            icon: 'success',
            confirmButtonText: 'OK'
          }).then((e) => {
            console.log(response);
            this.router.navigate(['/home']);
          })
        }
      })
      //navigate to grades
    }
    else{
      if (this.QuestionUI?.id == this.SelectedAnswer?.q_ID) {
        this.correctAnswers++;
      }
      this.createAnswersArray();
    }


    console.log(this.correctAnswers);
  }

}
