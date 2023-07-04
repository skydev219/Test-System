import { TokenService } from 'src/app/Services/token.service';
import { Grade } from 'src/app/Interfaces/All';
import { GradeService } from './../../../Services/grade.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-student-grades',
  templateUrl: './student-grades.component.html',
  styleUrls: ['./student-grades.component.css']
})
export class StudentGradesComponent implements OnInit {
  grades:Grade[]=[]

  constructor(private gradeService:GradeService,private tokenService:TokenService){}
  ngOnInit(): void {
    let id = this.tokenService.GetUserId();
    if (id) {
    this.gradeService.GetGradeByStudent(id).subscribe({
      next:(response:any) =>{
        this.grades=response.body;
        console.log(this.grades)

      },
      error: (e) => console.error(e),
      complete: () => console.info('Success'),
    });
      
    }
  }

}
