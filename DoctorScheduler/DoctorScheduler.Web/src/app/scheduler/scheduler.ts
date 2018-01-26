export interface IScheduler{
    Facility: IFacility,
    SlotDurationMinutes: number,
    Monday: ISlot,
    Tuesday: ISlot,
    Wednesday: ISlot,
    Thursday: ISlot,    
    Friday: ISlot,
    Saturday: ISlot,
    Sunday: ISlot
}

export interface IFacility {
    FacilityId: string;
    Name: string;
    Address: string;
}

export interface ISlot {
    WorkPeriod: IWorkPeriod;
    BusySlots: IBusySlots;
}

export interface IWorkPeriod {
    StartHour: number;
    EndHour: number;
    LunchStartHour: number;
    LunchEndHour: number;
}

export interface IBusySlots {
    Start: string;
    End: string;
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