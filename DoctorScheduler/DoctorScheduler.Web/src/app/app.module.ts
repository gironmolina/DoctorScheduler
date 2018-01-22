import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { SchedulerComponent } from './scheduler/scheduler.component';
import { FormComponent } from './form/form.component';
import { OcticonDirective } from './shared/octicon.directive';

@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    SchedulerComponent,
    FormComponent,
    OcticonDirective 
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      { path: 'welcome', component: WelcomeComponent },
      { path: 'scheduler', component: SchedulerComponent },
      { path: 'form', component: FormComponent },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
