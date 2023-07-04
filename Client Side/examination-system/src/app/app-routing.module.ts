import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { StudentLoginComponent } from './components/student-login/student-login.component';
import { StudentRegisterComponent } from './components/student-register/student-register.component';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { ExamComponent } from './components/exam/exam.component';
import { AdminLoginComponent } from './components/admin/admin-login/admin-login.component';
import { AdminHomeComponent } from './components/admin/admin-home/admin-home.component';
import { StudentGradesComponent } from './components/grade/student-grades/student-grades.component';
import { StudentProfileComponent } from './components/student-profile/student-profile.component';
import { EditExamComponent } from './components/edit-exam/edit-exam.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'student/login', component: StudentLoginComponent },
  { path: 'student/register', component: StudentRegisterComponent },
  { path: 'exams/:id', component: ExamComponent },
  { path: 'admin/login', component: AdminLoginComponent },
  { path: 'admin/home', component: AdminHomeComponent },
  { path: 'student/profile', component: StudentProfileComponent },
  {path:'student/grades/:id',component:StudentGradesComponent},
  {path:'edit/exam/:id',component:EditExamComponent},
  { path: '**', component: NotfoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
