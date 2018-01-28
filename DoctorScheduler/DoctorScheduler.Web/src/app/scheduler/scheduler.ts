export interface IScheduler{
    Facility: IFacility,
    SlotDurationMinutes: number,
    WeekHours: IWeekHours[]
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