import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CurrencyService } from '../Service/Currency.service';
import { CurrencyDto } from './CurrencyDto';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'currency-select',
  imports: [CommonModule],
  templateUrl: './Currency.html',
  //styleUrls: ['./Currency.css']
})
export class CurrencyComponent implements OnInit {
  private currencyService = inject(CurrencyService);
  currencies: CurrencyDto[] = [];
    dataFetch$!: Observable<any[]>;
  ngOnInit(): void {
    this.dataFetch$ = this.currencyService.getAll();
    this.dataFetch$.subscribe({
      next: (data) => this.currencies = data,
      error: (err) => console.error('Failed to load currencies', err)
    });
  }
}