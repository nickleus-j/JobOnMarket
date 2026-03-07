import { Component, OnInit, Input, Output, EventEmitter,NgModule } from '@angular/core';
import { FormBuilder, FormGroup, Validators ,ReactiveFormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CurrencyService } from '../Service/Currency.service';
import { CurrencyDto } from '../Currency/CurrencyDto';
import { JobDto } from './JobDto';
import { JobFormService } from '../Service/Job.Form.Service';
@Component({
  selector: 'job-form',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './Job.Form.html',
  styleUrls: ['./Job.Form.css'],
})
export class JobFormComponent implements OnInit {
  @Input() job: JobDto | null = null;
  @Output() jobSaved = new EventEmitter<JobDto>();
  @Output() cancelled = new EventEmitter<void>();

  jobForm!: FormGroup;
  currencies!: CurrencyDto[];
  currencies$!: Observable<CurrencyDto[]>;
  isSubmitting = false;
  errorMessage: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private currencyService: CurrencyService,
    private jobService: JobFormService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadCurrencies();
  }

  private initializeForm(): void {
    this.jobForm = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      startDate: [null, [Validators.required]],
      dueDate: [null, [Validators.required]],
      budget: ['', [Validators.required, Validators.min(0)]],
      currencyCode: ['', [Validators.required]],
      description: ['', [Validators.required, Validators.minLength(10)]]
    });

    // Populate form if editing existing job
    if (this.job) {
      this.jobForm.patchValue({
        id: this.job.id,
        startDate: this.job.startDate,
        dueDate: this.job.dueDate,
        budget: this.job.budget,
        currencyCode: this.job.currencyCode,
        description: this.job.description
      });
    }
  }

  private loadCurrencies(): void {
     this.currencies$ = this.currencyService.getAll();
     this.currencies$.subscribe({
      next: (data) => this.currencies = data,
      error: (err) => console.error('Failed to load currencies', err)
    });
  }

  onSubmit(): void {
    if (this.jobForm.invalid) {
      this.markFormGroupTouched(this.jobForm);
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.jobForm.getRawValue();

    const jobData: JobDto = {
      id: formValue.id || 0,
      startDate: new Date(formValue.startDate),
      dueDate: new Date(formValue.dueDate),
      budget: parseFloat(formValue.budget),
      currencyCode: formValue.currencyCode,
      description: formValue.description
    };

    const request$ = jobData.id === 0 || isNaN(jobData.id )
      ? this.jobService.create(jobData)
      : this.jobService.update(jobData);

    request$.subscribe({
      next: (response) => {
        this.isSubmitting = false;
        this.jobSaved.emit(response);
        this.router.navigate(['/jobs']); 
      },
      error: (error) => {
        this.isSubmitting = false;
        this.errorMessage = this.getServerErrorMessage(error);
        console.error('Error saving job:', error);
      }
    });
  }

  onCancel(): void {
    this.cancelled.emit();
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
    });
  }

  private formatDateForInput(date: Date | string): string {
    if (!date) return '';
    const d = new Date(date);
    const year = d.getFullYear();
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  private getServerErrorMessage(error: any): string {
    if (error.status === 400) {
      return error.error?.message || 'Invalid job data. Please check your input.';
    }
    if (error.status === 401) {
      return 'You are not authorized to perform this action.';
    }
    if (error.status === 409) {
      return 'A job with this ID already exists.';
    }
    if (error.status >= 500) {
      return 'Server error. Please try again later.';
    }
    return 'An error occurred while saving the job. Please try again.';
  }

  getControl(name: string) {
    return this.jobForm.get(name);
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.jobForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldErrorMessage(fieldName: string): string {
    const control = this.jobForm.get(fieldName);
    if (!control?.errors) return '';

    if (control.hasError('required')) {
      return `${this.formatFieldName(fieldName)} is required`;
    }
    if (control.hasError('min')) {
      return `${this.formatFieldName(fieldName)} must be greater than or equal to 0`;
    }
    if (control.hasError('minlength')) {
      const requiredLength = control.getError('minlength').requiredLength;
      return `${this.formatFieldName(fieldName)} must be at least ${requiredLength} characters`;
    }
    return 'Invalid input';
  }

  private formatFieldName(name: string): string {
    return name
      .replace(/([A-Z])/g, ' $1')
      .replace(/^./, str => str.toUpperCase())
      .trim();
  }
}
