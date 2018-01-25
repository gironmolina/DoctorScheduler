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
    var mondayDate = this.getMonday(new Date());
    var dateFormat = this.convertDate(mondayDate);
    this.getScheduler(dateFormat);
  }

  private getScheduler(date : string): void {
    this._schedulerService.getSchedulers(date)
            .subscribe(scheduler => { this.scheduler = scheduler                    
            },
            error => this.errorMessage = <any>error);
  }

  private previousDate(): void {
    console.log("previousDate");
  }

  private nextDate(): void {
    var currentDate = new Date();
    currentDate.setDate(currentDate.getDate() + (1 + 7 - currentDate.getDay()) % 7);
    var qqq = this.convertDate(currentDate);
    console.log(qqq);
  }

  getMonday(date: Date): Date {
    var currentDate = new Date(date);
    var dayOfTheWeek = currentDate.getDay();
    var mondayDay = currentDate.getDate() - dayOfTheWeek + (dayOfTheWeek == 0 ? -6 : 1);
    return new Date(currentDate.setDate(mondayDay));
  }

  getNextDayOfWeek(date, dayOfWeek): string {
    // Code to check that date and dayOfWeek are valid left as an exercise ;)
    var resultDate = new Date(date.getTime());
    var asd = date.getDate();
    resultDate.setDate(date.getDate() + (7 + dayOfWeek - date.getDay()) % 7);

    var qqq = this.convertDate(resultDate);

    return resultDate.toString();
  }

  convertDate(asd : Date): string {
    var mm = asd.getMonth() + 1; // getMonth() is zero-based
    var dd = asd.getDate();
  
    return [asd.getFullYear(),
            (mm>9 ? '' : '0') + mm,
            (dd>9 ? '' : '0') + dd
           ].join('');
  };
}