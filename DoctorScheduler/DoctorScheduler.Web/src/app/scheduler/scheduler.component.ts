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
  isLoading: boolean = true;
  pageTitle: string = 'Scheduler';
  scheduler: IScheduler;
  WeekHours: IWeekHours[];  
  name: string;  
  address: string; 
  errorMessage: string;
  currentDate: Date = new Date();

  constructor(private _schedulerService: SchedulerService,
              private _router: Router) {
  }

  ngOnInit(): void {     
    this.getScheduler(this.currentDate);    
  }  

  onNextDate(isNext : boolean): void {
    this.isLoading = true;
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
              this.WeekHours = this.scheduler.WeekHours;
              this.name = this.scheduler.Facility.Name;
              this.address = this.scheduler.Facility.Address;
              this.isLoading = false;              
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