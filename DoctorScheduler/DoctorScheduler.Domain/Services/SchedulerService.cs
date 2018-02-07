using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorScheduler.CrossCutting.Enums;
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
            var schedulerEntity = await this.schedulerRepository.GetScheduler(date);
            var schedulerWeek = this.GetSchedulerWeek(schedulerEntity);

            for (var i = schedulerWeek.WeekHours.Count - 1; i >= 0; i--)
            {
                if (schedulerWeek.WeekHours[i].Monday == null &&
                    schedulerWeek.WeekHours[i].Tuesday == null &&
                    schedulerWeek.WeekHours[i].Wednesday == null &&
                    schedulerWeek.WeekHours[i].Thursday == null &&
                    schedulerWeek.WeekHours[i].Friday == null &&
                    schedulerWeek.WeekHours[i].Saturday == null &&
                    schedulerWeek.WeekHours[i].Sunday == null)
                {
                    schedulerWeek.WeekHours.Remove(schedulerWeek.WeekHours[i]);
                }
            }

            return schedulerWeek;
        }

        public async Task<bool> TakeSlot(TakeSlotEntity slot)
        {
            return await this.schedulerRepository.PostSlot(slot);
        }

        private SchedulerWeekEntity GetSchedulerWeek(SchedulerEntity schedulerEntity)
        {
            var schedulerDictionary = this.GetWeekDictionary(schedulerEntity);
            var minHour = schedulerDictionary.Min(i => i.Value?.WorkPeriod?.StartHour) ?? 0;
            var maxHour = schedulerDictionary.Max(i => i.Value?.WorkPeriod?.EndHour) ?? 23;

            var initialHour = new TimeSpan(minHour, 0, 0);
            var finalHour = new TimeSpan(maxHour, 0, 0);
            var slotDuration = schedulerEntity.SlotDurationMinutes;

            var hourRows = new List<WeekHoursEntity>();
            for (var hour = initialHour; hour <= finalHour; hour = hour.Add(new TimeSpan(0, slotDuration, 0)))
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
                this.ValidateSlot(DaysEnum.Monday, hourRow, schedulerDictionary);
                this.ValidateSlot(DaysEnum.Tuesday, hourRow, schedulerDictionary);
                this.ValidateSlot(DaysEnum.Wednesday, hourRow, schedulerDictionary);
                this.ValidateSlot(DaysEnum.Thursday, hourRow, schedulerDictionary);
                this.ValidateSlot(DaysEnum.Friday, hourRow, schedulerDictionary);
                this.ValidateSlot(DaysEnum.Saturday, hourRow, schedulerDictionary);
                this.ValidateSlot(DaysEnum.Sunday, hourRow, schedulerDictionary);
            }

            return new SchedulerWeekEntity
            {
                Facility = schedulerEntity.Facility,
                SlotDurationMinutes = schedulerEntity.SlotDurationMinutes,
                WeekHours = hourRows
            };
        }

        private void ValidateSlot(DaysEnum day, WeekHoursEntity hourRow, Dictionary<int, SlotEntity> schedulerDictionary)
        {
            var dayProperty = hourRow.GetType().GetProperty(day.ToString());
            var hour = (TimeSpan?)dayProperty?.GetValue(hourRow);

            var dayInfo = schedulerDictionary.FirstOrDefault(i => i.Key == (int)day).Value;
            if (dayInfo != null)
            {
                var isBetweenRange1 = hour.IsBetween(new TimeSpan(dayInfo.WorkPeriod.StartHour, 0, 0),
                                                     new TimeSpan(dayInfo.WorkPeriod.LunchStartHour, 0, 0));
                var isBetweenRange2 = hour.IsBetween(new TimeSpan(dayInfo.WorkPeriod.LunchEndHour, 0, 0),
                                                     new TimeSpan(dayInfo.WorkPeriod.EndHour, 0, 0));

                var isBusySlot = false;
                if (dayInfo.BusySlots != null)
                {
                    isBusySlot = dayInfo.BusySlots.Any(busyHour => hour.IsBetween(busyHour.Start.TimeOfDay,
                                                                   busyHour.End.TimeOfDay));
                }

                // Validate if it's an available slot
                if (!isBetweenRange1 && !isBetweenRange2 || isBusySlot)
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
