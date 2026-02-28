import { Component } from '@angular/core';
import { AuthService } from '../Service/auth.service';
import { CommonModule } from '@angular/common';
import { LogIn } from '../login/login';
import { RegisterComponent } from '../register.component/register.component';
@Component({
  selector: 'home',
  imports: [CommonModule,LogIn,RegisterComponent],
  templateUrl: './home.html',
  styleUrl:'./home.css'
})
export class Home {
  constructor(public authService: AuthService) {}
  
}