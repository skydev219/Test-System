import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { StudentLoginComponent } from './components/student-login/student-login.component';
import { StudentRegisterComponent } from './components/student-register/student-register.component';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { ExamComponent } from './components/exam/exam.component';

const routes: Routes = [
{path:'',component:HomeComponent},
{path:'home',component:HomeComponent},
{path:'student/login',component:StudentLoginComponent},
{path:'student/register',component:StudentRegisterComponent},
{path:'exam',component:ExamComponent},
{path:'**',component:NotfoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
