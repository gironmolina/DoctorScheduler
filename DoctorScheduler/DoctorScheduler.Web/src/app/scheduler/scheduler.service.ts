import { Injectable } from "@angular/core";
import { IScheduler, Slot } from "./scheduler";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Response } from '@angular/http';
import { Observable } from "rxjs/Observable";
import { HttpErrorResponse } from "@angular/common/http/src/response";
import { AppSettings } from '../app.settings';
import "rxjs/add/observable/throw";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/do";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/share';
const SCHEDULER_ENDPOINT = `${AppSettings.API_ENDPOINT}`;

@Injectable()
export class SchedulerService {    
    constructor(private _http: HttpClient) {
    }

    getSchedulers(date: string): Observable<IScheduler> {
        return this._http.get<IScheduler>(`${SCHEDULER_ENDPOINT}availability/?date=${date}`)
            .catch(this.handleError);
    }

    postSlot(body: Slot): void{
        let url = `${SCHEDULER_ENDPOINT}takeSlot`;
        this._http.post(url, body)
        .subscribe();
    }

    private handleError(err: HttpErrorResponse) {
        console.log(err.message);
        return Observable.throw(err.message);
    }    
}
