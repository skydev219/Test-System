import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgProgressModule } from 'ngx-progressbar'
import { AuthInterceptor } from './Interceptor/auth.interceptor';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { StudentLoginComponent } from './components/student-login/student-login.component';
import { StudentRegisterComponent } from './components/student-register/student-register.component';
import { ExamComponent } from './components/exam/exam.component';
import { FooterComponent } from './components/footer/footer.component';
import { AdminLoginComponent } from './components/admin/admin-login/admin-login.component';
import { AdminHomeComponent } from './components/admin/admin-home/admin-home.component';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ToastModule } from 'primeng/toast';
import { TableModule } from 'primeng/table';
import { StudentGradesComponent } from './components/grade/student-grades/student-grades.component';
import { StudentProfileComponent } from './components/student-profile/student-profile.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
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
    StudentGradesComponent,
    StudentProfileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    RadioButtonModule,
    FormsModule,
    ToastModule,
    TableModule,
    NgProgressModule,
    MatProgressSpinnerModule,
    BrowserAnimationsModule,
    MatSlideToggleModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
