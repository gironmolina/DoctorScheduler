import { Component } from '@angular/core';
import { SchedulerService } from './scheduler/scheduler.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [SchedulerService]
})

export class AppComponent {
  pageTitle: string = 'Doctor Scheduler';
}
