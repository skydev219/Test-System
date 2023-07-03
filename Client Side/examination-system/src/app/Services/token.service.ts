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
  SaveToken(token: string, role: string) {
    localStorage.setItem('Token', token);
    localStorage.setItem('Role', role);
  }
  ClearToken() {
    localStorage.clear();
  }
}
