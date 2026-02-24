import { Routes } from '@angular/router';
import { RegisterComponent } from '../register.component/register.component';
import { CurrencyComponent } from '../Currency/Currency';
export const routes: Routes = [
    { path: 'register', component: RegisterComponent },
    { path: 'dashboard', component: CurrencyComponent },
  ];
