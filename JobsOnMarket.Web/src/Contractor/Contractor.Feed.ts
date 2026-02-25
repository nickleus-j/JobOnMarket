import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs/internal/Observable';
import { ContractorFeedService } from '../Service/Contractor.Feed.service';
import { ContractorDto } from './ContractorDto';
@Component({
  selector: 'contractor-feed',
  imports: [CommonModule],
  templateUrl: './Contractor.Feeed.html',
//   styleUrls: ['./Job.Feed.css']
})
export class ContractorFeed implements OnInit {
  private feedService = inject(ContractorFeedService);
    dtos: ContractorDto[] = [];
      dataFetch$!: Observable<ContractorDto[]>;
    ngOnInit(): void {
      this.dataFetch$ = this.feedService.get();
      this.dataFetch$.subscribe({
        next: (data) => this.dtos = data,
        error: (err) => console.error('Failed to load contractors', err)
      });
    }
}