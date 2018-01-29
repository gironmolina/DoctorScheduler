import { IScheduler, IWeekHours } from './Scheduler';
import { Component, OnInit } from '@angular/core';
import { SchedulerService } from "./scheduler.service";
import { forEach } from '@angular/router/src/utils/collection';
import { Router } from '@angular/router';

@Component({
  templateUrl: './scheduler.component.html',
  styleUrls: ['./scheduler.component.css']
})

export class SchedulerComponent implements OnInit {
  pageTitle: string = 'Scheduler';
  scheduler: IScheduler;
  errorMessage: string;
  currentDate: Date  = new Date();
  source: IWeekHours[] = [];

  constructor(private _schedulerService: SchedulerService,
              private _router: Router) {
  }

  ngOnInit(): void {     
    this.getScheduler(this.currentDate);
    this.fillScheduler();  
  }  

  fillScheduler(): void{
  for (var i = this.scheduler.WeekHours.length - 1; i >= 0; i--) {
    if (this.scheduler.WeekHours[i].Monday === null &&
        this.scheduler.WeekHours[i].Tuesday === null &&
        this.scheduler.WeekHours[i].Wednesday === null &&
        this.scheduler.WeekHours[i].Thursday === null &&
        this.scheduler.WeekHours[i].Friday === null &&
        this.scheduler.WeekHours[i].Saturday === null &&
        this.scheduler.WeekHours[i].Sunday === null)
      {
          this.scheduler.WeekHours.splice(i, 1);
      }
    }
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
    this.getScheduler(this.currentDate);
  }  

  getScheduler(date : Date): void {
    var mondayDate = this.getMonday(date);
    var dateFormat = this.convertDate(mondayDate);    
    this._schedulerService.getSchedulers(dateFormat)
            .subscribe(scheduler => {
              this.scheduler = scheduler;
              this.fillScheduler();
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

  onGoToForm(time: number){
    this._router.navigate(["/form", 
                            this.scheduler.Facility.FacilityId, 
                            this.currentDate.getFullYear(),
                            this.currentDate.getMonth(),
                            this.currentDate.getDate(),
                            time
                          ]);
  }
}