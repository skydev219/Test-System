import { environment } from './../../Environment/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TokenService } from 'src/app/Services/token.service';
import { ExamService } from '../../Services/exam.service';
import { Exam } from '../../Interfaces/All';
import { reduce } from 'rxjs';
import { StudentService } from 'src/app/Services/student.service';

@Component({
  selector: 'app-exam',
  templateUrl: './exam.component.html',
  styleUrls: ['./exam.component.css'],
})
export class ExamComponent {
  examId?: number;
  exam: Exam | undefined;
  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private examService: ExamService,
    private studentService: StudentService
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
        },
        error: (e) => console.error(e),
        complete: () => console.info('Success'),
      });
    }
  }
}
