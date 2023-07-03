import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { IUser } from '../Interfaces/iuser';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { environment } from '../Environment/environment';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private router: Router, private http: HttpClient) {}
  currentUser: IUser | undefined;
  private options = {
    observe: 'response' as const,
    headers: new HttpHeaders().set('Content-Type', 'application/json'),
  };

  loginAdmin(username: string, password: string) {
    this.currentUser = { username: username, password: password };
    return this.http.post(
      environment.contentful.loginAdmin,
      this.currentUser,
      this.options
    );
  }

  logInStudent(user: string | null, pass: string | null) {
    this.currentUser = { username: user, password: pass };
    return this.http.post(
      environment.contentful.loginStudent,
      this.currentUser,
      this.options
    );
  }
  logOut() {
    this.currentUser = undefined;
    this.router.navigate(['/login']);
  }
  get isLoggedIn(): boolean {
    if (this.currentUser?.username && this.currentUser?.password) {
      return true;
    } else {
      return false;
    }
  }
}
