import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor() {}
  GetToken() {
    return localStorage.getItem('Token');
  }
  SaveToken(token: string) {
    return localStorage.setItem('Token', token);
  }
  ClearToken() {
    localStorage.removeItem('Token');
  }
}
