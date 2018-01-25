import { IScheduler } from './Scheduler';
import { Component, OnInit } from '@angular/core';
import { SchedulerService } from "./scheduler.service";

@Component({
  templateUrl: './scheduler.component.html',
  styleUrls: ['./scheduler.component.css']
})

export class SchedulerComponent implements OnInit {
  pageTitle: string = 'Scheduler';
  scheduler: IScheduler;
  errorMessage: string;
  
  constructor(private _schedulerService: SchedulerService) {
  }

  ngOnInit(): void {
    this._schedulerService.getSchedulers("20180101")
            .subscribe(scheduler => { this.scheduler = scheduler                    
            },
            error => this.errorMessage = <any>error);
  }

  previousDate(): void {
    console.log("previousDate");
  }

  nextDate(): void {
      console.log("nextDate");
      var d = new Date();
      d.setDate(d.getDate() + (1 + 7 - d.getDay()) % 7);
      console.log(d);
  }

  getNextDayOfWeek(date, dayOfWeek): string {
    // Code to check that date and dayOfWeek are valid left as an exercise ;)

    var resultDate = new Date(date.getTime());

    resultDate.setDate(date.getDate() + (7 + dayOfWeek - date.getDay()) % 7);

    return resultDate.toString();
  }
}