import { Routes } from '@angular/router';
import { RegisterComponent } from '../register.component/register.component';
import { CurrencyComponent } from '../Currency/Currency';
import { LogIn } from '../login/login';
import { Home } from '../home/home';
import { JobFeed } from '../JobFeed/Job.Feed';
import { ContractorFeed } from '../Contractor/Contractor.Feed';
import { JobDetail } from '../Job/Job.component';
import { JobFormComponent } from '../Job/Job.Form';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'login', component: LogIn },
    { path: 'register', component: RegisterComponent },
    { path: 'dashboard', component: CurrencyComponent },
    {path:'jobs',component:JobFeed},
    {path:'job/:id',component:JobDetail},
    {path:'jobform',component:JobFormComponent},
    {path:'contractors',component:ContractorFeed}
  ];
