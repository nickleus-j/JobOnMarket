// app.module.ts (or your feature module)
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { JobFormComponent } from './Job.Form';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule,JobFormComponent],
  exports: [JobFormComponent]
})
export class JobModule { }
