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
  currentDate: Date;
  
  constructor(private _schedulerService: SchedulerService) {
  }

  ngOnInit(): void {
    this.currentDate = new Date();
    var mondayDate = this.getMonday(this.currentDate);
    var dateFormat = this.convertDate(mondayDate);
    this.getScheduler(dateFormat);
  }

  getDate(isNext : boolean): void {
    var date = new Date(this.currentDate);
    if (isNext) {
      date.setDate(date.getDate() + (7 - date.getDay()) % 7 + 1);
    }
    else {
      date.setDate(date.getDate() - (date.getDay() + 6));
    }    
    this.currentDate = date;
    var dateFormat = this.convertDate(date);
    this.getScheduler(dateFormat);
    console.log(dateFormat);
  }  

  getScheduler(date : string): void {
    this._schedulerService.getSchedulers(date)
            .subscribe(scheduler => { this.scheduler = scheduler                    
            },
            error => this.errorMessage = <any>error);
  }

  getMonday(date: Date): Date {
    var currentDate = new Date(date);
    var dayOfTheWeek = currentDate.getDay();
    var mondayDay = currentDate.getDate() - dayOfTheWeek + (dayOfTheWeek == 0 ? -6 : 1);
    return new Date(currentDate.setDate(mondayDay));
  }

  convertDate(date : Date): string {
    var month = date.getMonth() + 1;
    var day = date.getDate();  
    return [date.getFullYear(),
            (month > 9 ? '' : '0') + month,
            (day > 9 ? '' : '0') + day
           ].join('');
  };
}