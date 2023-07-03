import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor() {}
  GetToken() {
    return localStorage.getItem('Token');
  }
  GetRole() {
    return localStorage.getItem('Role');
  }
  GetUsername() {
    return localStorage.getItem('Username');
  }
  SaveToken(token: string, role: string, username: string) {
    localStorage.setItem('Token', token);
    localStorage.setItem('Role', role);
    localStorage.setItem('Username', username);
  }
  ClearToken() {
    localStorage.clear();
  }
}
