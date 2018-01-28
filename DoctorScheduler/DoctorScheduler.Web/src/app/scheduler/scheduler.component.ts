import { IScheduler, IWeekHours } from './Scheduler';
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
  currentDate: Date  = new Date();
  source: IWeekHours[] = [];
  filterSource: IWeekHours[];

  constructor(private _schedulerService: SchedulerService) {
  }

  ngOnInit(): void {     
    this.getScheduler(this.currentDate);
    this.fillScheduler();  
  }  

  fillScheduler(): void{
    for (var i = 0; i < 24; i++) 
    {
      var timeFormat = (i > 9 ? '' : '0') + i + (':00');
      this.source.push({
        Monday: timeFormat, 
        Tuesday: timeFormat, 
        Wednesday: timeFormat, 
        Thursday: timeFormat, 
        Friday: timeFormat, 
        Saturday: timeFormat, 
        Sunday: timeFormat
      });    
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
    this.fillScheduler();
  }  

  getScheduler(date : Date): void {
    var mondayDate = this.getMonday(date);
    var dateFormat = this.convertDate(mondayDate);    
    this._schedulerService.getSchedulers(dateFormat)
            .subscribe(scheduler => {
              this.scheduler = scheduler;
              this.loadPurgeDetails();
            },
            error => this.errorMessage = <any>error);    
  }

  private loadPurgeDetails() {
    for (var i = 0; i < this.source.length; i++) 
    {
      //******** Monday **********/

      if (this.scheduler.Monday !== null){
        var a1 = this.between(i, 0, this.scheduler.Monday.WorkPeriod.StartHour);
        var a2 = this.between(i, this.scheduler.Monday.WorkPeriod.LunchStartHour, 
                                this.scheduler.Monday.WorkPeriod.LunchEndHour);
        var a3 = this.between(i, this.scheduler.Monday.WorkPeriod.EndHour, 24);

        if (a1 || a2 || a3){
          this.source[i].Monday = null;
        }
      }
      else{
        this.source[i].Tuesday = null;
      }

      //******** Tuesday **********/

      if (this.scheduler.Tuesday !== null){
        var b1 = this.between(i, 0, this.scheduler.Tuesday.WorkPeriod.StartHour);
        var b2 = this.between(i, this.scheduler.Tuesday.WorkPeriod.LunchStartHour, 
                                this.scheduler.Tuesday.WorkPeriod.LunchEndHour);
        var b3 = this.between(i, this.scheduler.Tuesday.WorkPeriod.EndHour, 24);

        if (b1 || b2 || b3){
          this.source[i].Tuesday = null;
        }
      }
      else{
        this.source[i].Tuesday = null;
      }

      //******** Wednesday **********/

      if (this.scheduler.Wednesday !== null){
        var c1 = this.between(i, 0, this.scheduler.Wednesday.WorkPeriod.StartHour);
        var c2 = this.between(i, this.scheduler.Wednesday.WorkPeriod.LunchStartHour, 
                                this.scheduler.Wednesday.WorkPeriod.LunchEndHour);
        var c3 = this.between(i, this.scheduler.Wednesday.WorkPeriod.EndHour, 24);

        if (c1 || c2 || c3){
          this.source[i].Wednesday = null;
        }
      }
      else{
        this.source[i].Wednesday = null;
      }

      //******** Thursday **********/

      if (this.scheduler.Thursday !== null){
        var d1 = this.between(i, 0, this.scheduler.Thursday.WorkPeriod.StartHour);
        var d2 = this.between(i, this.scheduler.Thursday.WorkPeriod.LunchStartHour, 
                                this.scheduler.Thursday.WorkPeriod.LunchEndHour);
        var d3 = this.between(i, this.scheduler.Thursday.WorkPeriod.EndHour, 24);

        if (d1 || d2 || d3){
          this.source[i].Thursday = null;
        }
      }
      else{
        this.source[i].Thursday = null;
      }

      //******** Friday **********/

      if (this.scheduler.Friday !== null){
        var e1 = this.between(i, 0, this.scheduler.Friday.WorkPeriod.StartHour);
        var e2 = this.between(i, this.scheduler.Friday.WorkPeriod.LunchStartHour, 
                                this.scheduler.Friday.WorkPeriod.LunchEndHour);
        var e3 = this.between(i, this.scheduler.Friday.WorkPeriod.EndHour, 24);
        var e4 = this.between(i, this.scheduler.Friday.BusySlots.Start, 
          this.scheduler.Friday.BusySlots.End);

        if (e1 || e2 || e3 || e4){
          this.source[i].Friday = null;
        }

         

      }
      else{
        this.source[i].Friday = null;
      }

      //******** Saturday **********/

      if (this.scheduler.Saturday !== null){
        var f1 = this.between(i, 0, this.scheduler.Saturday.WorkPeriod.StartHour);
        var f2 = this.between(i, this.scheduler.Saturday.WorkPeriod.LunchStartHour, 
                                this.scheduler.Saturday.WorkPeriod.LunchEndHour);
        var f3 = this.between(i, this.scheduler.Saturday.WorkPeriod.EndHour, 24);

        if (f1 || f2 || f3){
          this.source[i].Saturday = null;
        }
      }
      else{
        this.source[i].Saturday = null;
      }

      //******** Sunday **********/

      if (this.scheduler.Sunday !== null){
        var g1 = this.between(i, 0, this.scheduler.Sunday.WorkPeriod.StartHour);
        var g2 = this.between(i, this.scheduler.Sunday.WorkPeriod.LunchStartHour, 
                                this.scheduler.Sunday.WorkPeriod.LunchEndHour);
        var g3 = this.between(i, this.scheduler.Sunday.WorkPeriod.EndHour, 24);

        if (g1 || g2 || g3){
          this.source[i].Sunday = null;
        }
      }
      else{
        this.source[i].Sunday = null;
      }
    };

    this.cleanRows();
    this.filterSource = this.source; 
    this.source = [];   
  }

  between(x, min, max): boolean {
    return x >= min && x < max;
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

  cleanRows(): void{
    for (var i = this.source.length - 1; i >= 0; i--) {
      if (this.source[i].Monday === null &&
          this.source[i].Tuesday === null &&
          this.source[i].Wednesday === null &&
          this.source[i].Thursday === null &&
          this.source[i].Friday === null &&
          this.source[i].Saturday === null &&
          this.source[i].Sunday === null)
        {
            this.source.splice(i, 1);
        }
    }
  }
}