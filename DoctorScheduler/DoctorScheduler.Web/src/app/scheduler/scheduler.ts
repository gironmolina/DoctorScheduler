export interface IScheduler{
    Facility: IFacility;
    SlotDurationMinutes: number;
    WeekHours: IWeekHours[];
}

export interface IFacility {
    FacilityId: string;
    Name: string;
    Address: string;
}

export interface IWeekHours {
    Monday: number;
    Tuesday: number;
    Wednesday: number;
    Thursday: number;
    Friday: number;
    Saturday: number;
    Sunday: number;
}

export class Slot {
    FacilityId: string;
    Start: Date;
    End: Date;
    Patient: Patient;
    Comments: string;
}

export class Patient {
    Name: string;
    SecondName: string;
    Email: string;
    Phone: string;
}