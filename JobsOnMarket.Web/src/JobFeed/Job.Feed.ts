import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs/internal/Observable';
import { JobFeedService } from '../Service/Job.Feed.service';
import { JobDto } from '../Job/JobDto';
@Component({
  selector: 'job-feed',
  imports: [CommonModule],
  templateUrl: './Job.Feed.html',
  styleUrls: ['./Job.Feed.css']
})
export class JobFeed implements OnInit {
  private feedService = inject(JobFeedService);
  jobs: JobDto[] = [];
    dataFetch$!: Observable<JobDto[]>;
  ngOnInit(): void {
    this.dataFetch$ = this.feedService.getJobs();
    this.dataFetch$.subscribe({
      next: (data) => this.jobs = data,
      error: (err) => console.error('Failed to load jobs', err)
    });
  }
}
