import { Injectable } from "@angular/core";
import { IScheduler } from "./scheduler";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { HttpErrorResponse } from "@angular/common/http/src/response";
import { AppSettings } from '../app.settings';
import "rxjs/add/observable/throw";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/do";
import 'rxjs/add/operator/map';

const SCHEDULER_ENDPOINT = `${AppSettings.API_ENDPOINT}`;

@Injectable()
export class SchedulerService {    
    constructor(private _http: HttpClient) {
    }

    getSchedulers(date: string): Observable<IScheduler> {
        return this._http.get<IScheduler>(`${SCHEDULER_ENDPOINT}availability/?date=${date}`)
            .do(data => console.log("All: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        console.log(err.message);
        return Observable.throw(err.message);
    }    
}