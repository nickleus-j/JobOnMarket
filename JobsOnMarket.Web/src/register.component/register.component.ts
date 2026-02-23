import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // Required for *ngIf
import { Router, RouterModule } from '@angular/router'; // Required for routerLink
import { AuthService } from '../Service/auth.service';

@Component({
  selector: 'app-register',
  standalone: true, // Use standalone for modern Angular
  imports: [ReactiveFormsModule, CommonModule, RouterModule], 
  templateUrl: './register.component.html',
  //styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    // Initialize form with validation logic
    this.registerForm = this.fb.group({
      userName: ['', [Validators.required, Validators.email]],
      unhashedPassword: ['', [Validators.required, Validators.minLength(6)]],
      firstName: ['', [Validators.required]],
      surname: ['', [Validators.required]],
      roleName: ["customer", []],
    });
  }

  ngOnInit(): void {
    // Check if user is already authenticated using AuthService
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/dashboard']);
    }
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      this.authService.register(this.registerForm.value).subscribe({
        next: (response) => {
          console.log('Registration successful', response);
          this.router.navigate(['/dashboard']);
        },
        error: (err) => {
          console.error('Registration error', err);
          // Optional: Add a property to show error messages in the UI
        }
      });
    }
  }
}
