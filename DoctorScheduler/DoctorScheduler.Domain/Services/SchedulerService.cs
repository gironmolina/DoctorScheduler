using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DoctorScheduler.CrossCutting.Extensions;
using DoctorScheduler.Domain.Interfaces;
using DoctorScheduler.Entities;
using DoctorScheduler.Infrastructure.Interfaces;
using log4net;

namespace DoctorScheduler.Domain.Services
{
    public class SchedulerService : ISchedulerService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SchedulerService));
        private readonly ISchedulerRepository schedulerRepository;

        public SchedulerService(ISchedulerRepository schedulerRepository)
        {
            this.schedulerRepository = schedulerRepository ?? throw new ArgumentNullException(nameof(schedulerRepository));
        }

        public async Task<SchedulerWeekEntity> GetWeeklyAvailability(string date)
        {
            var schedulerEntity = await schedulerRepository.GetScheduler(date);
            var schedulerWeek = this.GetSchedulerWeek(schedulerEntity);

            //for (var day = initialDate.Date; day < finalDate; day = day.AddMinutes(slotDuration))
            //{

            //}

            //for (var i = schedulerWeek.WeekHours.Count - 1; i >= 0; i--)
            //{
            //    if (schedulerWeek.WeekHours[i].Monday == null &&
            //        schedulerWeek.WeekHours[i].Tuesday ==  null &&
            //        schedulerWeek.WeekHours[i].Wednesday == null &&
            //        schedulerWeek.WeekHours[i].Thursday == null &&
            //        schedulerWeek.WeekHours[i].Friday == null &&
            //        schedulerWeek.WeekHours[i].Saturday == null &&
            //        schedulerWeek.WeekHours[i].Sunday == null)
            //    {
            //        schedulerWeek.WeekHours.Remove(schedulerWeek.WeekHours[i]);
            //    }
            //}

            return null;
        }

        public async Task<bool> TakeSlot(TakeSlotEntity slot)
        {
            return await schedulerRepository.PostSlot(slot);
        }


        enum Days
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }

        private SchedulerWeekEntity GetSchedulerWeek(SchedulerEntity schedulerEntity)
        {
            var schedulerDictionary = this.GetWeekDictionary(schedulerEntity);
            var minHour = schedulerDictionary.Min(i => i.Value?.WorkPeriod?.StartHour) ?? 0;
            var maxHour = schedulerDictionary.Max(i => i.Value?.WorkPeriod?.EndHour) ?? 23;

            var initialHour = new DateTime().Date.AddHours(minHour);
            var finalHour = new DateTime().Date.AddHours(maxHour);
            //var slotDuration = schedulerEntity.SlotDurationMinutes;
            var slotDuration = 60;

            var hourRows = new List<WeekHoursEntity>();
            for (var hour = initialHour; hour <= finalHour; hour = hour.AddMinutes(slotDuration))
            {
                hourRows.Add(new WeekHoursEntity
                {
                    Monday = hour,
                    Tuesday = hour,
                    Wednesday = hour,
                    Thursday = hour,
                    Friday = hour,
                    Saturday = hour,
                    Sunday = hour
                });
            }

            foreach (var hourRow in hourRows)
            {
                ValidateSlot(Days.Monday, hourRow, schedulerDictionary);
                ValidateSlot(Days.Tuesday, hourRow, schedulerDictionary);
                ValidateSlot(Days.Wednesday, hourRow, schedulerDictionary);
                ValidateSlot(Days.Thursday, hourRow, schedulerDictionary);
                ValidateSlot(Days.Friday, hourRow, schedulerDictionary);
                ValidateSlot(Days.Saturday, hourRow, schedulerDictionary);
                ValidateSlot(Days.Sunday, hourRow, schedulerDictionary);
            }

            return null;




            //var minStartHour = schedulerDictionary.Min(i => i.Value?.WorkPeriod?.StartHour) ?? 0;
            //var maxEndHour = schedulerDictionary.Max(i => i.Value?.WorkPeriod?.EndHour) ?? 24;
            //var dayHours = new List<WeekHoursEntity>();

            //// Loop day hours
            //for (var hour = minStartHour; hour <= maxEndHour; hour++)
            //{
            //    var slotScheduler = new List<int?>();

            //    // Loop day indexes
            //    for (var dayIndex = 0; dayIndex <= 6; dayIndex++)
            //    {
            //        var currentDayInfo = schedulerDictionary.FirstOrDefault(i => i.Key == dayIndex).Value;
            //        if (currentDayInfo != null)
            //        {
            //            var isBetweenRange1 = hour.IsBetween(currentDayInfo.WorkPeriod.StartHour, 
            //                                                 currentDayInfo.WorkPeriod.LunchStartHour - 1);
            //            var isBetweenRange2 = hour.IsBetween(currentDayInfo.WorkPeriod.LunchEndHour, 
            //                                                 currentDayInfo.WorkPeriod.EndHour);

            //            // Validate if it's a busy slot
            //            if (currentDayInfo.BusySlots != null)
            //            {
            //                var isBusy = currentDayInfo.BusySlots.Any(busyHour => 
            //                    hour.IsBetween(busyHour.Start.Hour, busyHour.End.Hour));
            //                if (isBusy)
            //                {
            //                    slotScheduler.Add(null);
            //                    continue;
            //                }
            //            }

            //            // If it's an available slot add slot, else add null.
            //            if (isBetweenRange1 || isBetweenRange2)
            //            {
            //                slotScheduler.Add(hour);
            //            }
            //            else
            //            {
            //                slotScheduler.Add(null);
            //            }
            //        }
            //        else
            //        {
            //            slotScheduler.Add(null);
            //        }
            //    }

            //    dayHours.Add(new WeekHoursEntity
            //    {
            //        Monday = slotScheduler[0],
            //        Tuesday = slotScheduler[1],
            //        Wednesday = slotScheduler[2],
            //        Thursday = slotScheduler[3],
            //        Friday = slotScheduler[4],
            //        Saturday = slotScheduler[5],
            //        Sunday = slotScheduler[6],
            //    });
            //}

            //return new SchedulerWeekEntity
            //{
            //    Facility = schedulerEntity.Facility,
            //    SlotDurationMinutes = schedulerEntity.SlotDurationMinutes,
            //    WeekHours = dayHours
            //};
        }

        private void ValidateSlot(Days day, WeekHoursEntity hourRow, Dictionary<int, SlotEntity> schedulerDictionary)
        {
            var dayProperty = hourRow.GetType().GetProperty(day.ToString());
            var hour = (DateTime?)dayProperty?.GetValue(hourRow);

            var currentDayInfo = schedulerDictionary.FirstOrDefault(i => i.Key == (int)day).Value;
            if (currentDayInfo != null)
            {
                var isBetweenRange1 = hour.IsBetween(new DateTime().Date.AddHours(currentDayInfo.WorkPeriod.StartHour),
                                                    new DateTime().Date.AddHours(currentDayInfo.WorkPeriod.LunchStartHour));
                var isBetweenRange2 = hour.IsBetween(new DateTime().Date.AddHours(currentDayInfo.WorkPeriod.LunchEndHour),
                                                    new DateTime().Date.AddHours(currentDayInfo.WorkPeriod.EndHour));

                if (!isBetweenRange1 && !isBetweenRange2)
                {
                    dayProperty?.SetValue(hourRow, null);
                }
            }
            else
            {
                dayProperty?.SetValue(hourRow, null);
            }
        }

        private Dictionary<int, SlotEntity> GetWeekDictionary(SchedulerEntity schedulerEntity)
        {
            return new Dictionary<int, SlotEntity>
            {
                {0, schedulerEntity.Monday},
                {1, schedulerEntity.Tuesday},
                {2, schedulerEntity.Wednesday},
                {3, schedulerEntity.Thursday},
                {4, schedulerEntity.Friday},
                {5, schedulerEntity.Saturday},
                {6, schedulerEntity.Sunday},
            };
        }
    }
}
