import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AuthInterceptor } from './Interceptor/auth.interceptor';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { FormsModule, NgModel, ReactiveFormsModule } from '@angular/forms';
import { StudentLoginComponent } from './components/student-login/student-login.component';
import { StudentRegisterComponent } from './components/student-register/student-register.component';
import { ExamComponent } from './components/exam/exam.component';
import { FooterComponent } from './components/footer/footer.component';
import { AdminLoginComponent } from './components/admin/admin-login/admin-login.component';
import { AdminHomeComponent } from './components/admin/admin-home/admin-home.component';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ToastModule } from 'primeng/toast';
import { EditExamComponent } from './components/edit-exam/edit-exam.component';
import { TableModule } from 'primeng/table';
import { StudentGradesComponent } from './components/grade/student-grades/student-grades.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    NotfoundComponent,
    StudentLoginComponent,
    StudentRegisterComponent,
    ExamComponent,
    FooterComponent,
    AdminLoginComponent,
    AdminHomeComponent,
    EditExamComponent,
    StudentGradesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    RadioButtonModule,
    FormsModule,
    ToastModule,
    TableModule
    
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
