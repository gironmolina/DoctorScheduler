import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})

export class FormComponent implements OnInit {
  public pageTitle: string = 'Form';
  
  constructor(private _router: Router) { }

  ngOnInit() {
  }

  onBack(): void{
    this._router.navigate(["/scheduler"]);
  }
}