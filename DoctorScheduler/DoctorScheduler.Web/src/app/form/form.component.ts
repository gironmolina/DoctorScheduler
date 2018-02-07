import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { SchedulerService } from '../scheduler/scheduler.service';
import { Slot, Patient } from '../scheduler/scheduler';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})

export class FormComponent implements OnInit {
  public pageTitle: string = 'Form';
  name: string;
  lastName: string;
  email: string;
  phone: string;
  comments: string;
  
  constructor(private _route: ActivatedRoute,
              private _router: Router,
              private _schedulerService: SchedulerService) { }

  ngOnInit() {    
  }

  addSlot() {   
    var id = this._route.snapshot.paramMap.get('id');
    var year = +this._route.snapshot.paramMap.get('year');
    var month = +this._route.snapshot.paramMap.get('month');
    var day = +this._route.snapshot.paramMap.get('day');
    var time = this._route.snapshot.paramMap.get('time');
    var duration = +this._route.snapshot.paramMap.get('duration');

    var mapDate = `${year}-${month + 1}-${day} ${time}`;
    var startDate = new Date(mapDate);    
    var endDate = new Date(startDate);
    endDate.setMinutes (startDate.getMinutes() + duration);
    
    var patient = new Patient();
    patient.Name = this.name;
    patient.SecondName = this.lastName;
    patient.Email = this.email;
    patient.Phone = this.phone;

    var slot = new Slot();
    slot.FacilityId = id;
    slot.Start = startDate;
    slot.End = endDate;
    slot.Patient = patient;
    slot.Comments = this.comments;

    this._schedulerService.postSlot(slot);    
  }

  onBack(): void{
    this._router.navigate(["/scheduler"]);
  }
}