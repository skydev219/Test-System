import { Component, Input } from '@angular/core';
import { TokenService } from 'src/app/Services/token.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  @Input() username: string | null = '';
  constructor(private tokenService: TokenService) {}

  get getUsername(): any {
    return (this.username = this.tokenService.GetUsername());
  }
  get logout(): void {
    return this.tokenService.ClearToken();
  }
}
