import { Component,  inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JobFeedService } from '../Service/Job.Feed.service';
import { JobDetailDto } from './JobDetailDto';
import { ActivatedRoute } from '@angular/router'; 
import { ContractorFeedService } from '../Service/Contractor.Feed.service';
import { Observable } from 'rxjs';
@Component({
  selector: 'job-detail',
  imports: [CommonModule],
  templateUrl: './Job.html',
  styleUrls: ['./Job.css']
})
export class JobDetail  {
  constructor(private route: ActivatedRoute, private service: JobFeedService) {}
    job!: JobDetailDto;
    dataFetch$!: Observable<JobDetailDto>;
    ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
        this.dataFetch$ = this.service.getJob(+id);
        this.dataFetch$.subscribe({
            next: (data) => this.job = data,
            error: (err) => console.error('Failed to load job details', err)
        });
        }
    }
}
