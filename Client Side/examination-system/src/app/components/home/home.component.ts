import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Output } from '@angular/core';
import { environment } from 'src/app/Environment/environment';
import { Exam, ExamService } from '../../Services/exam.service';

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
  constructor(private http: HttpClient, private examService: ExamService) {}
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
}
