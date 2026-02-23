import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { AuthService } from '../Service/auth.service';
import { CommonModule } from '@angular/common';
import { LogIn } from '../login/login';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterModule, CommonModule,LogIn],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  constructor(public authService: AuthService) {}
  protected readonly title = signal('JobsOnMarket');
  onLogout() {
    this.authService.logout();
  }
}
