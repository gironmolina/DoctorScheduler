using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
            return this.GetSchedulerWeek(schedulerEntity);
        }

        public async Task<bool> TakeSlot(TakeSlotEntity slot)
        {
            return await schedulerRepository.PostSlot(slot);
        }

        private SchedulerWeekEntity GetSchedulerWeek(SchedulerEntity schedulerEntity)
        {
            var schedulerDictionary = this.GetWeekDictionary(schedulerEntity);
            var minStartHour = schedulerDictionary.Min(i => i.Value?.WorkPeriod?.StartHour) ?? 0;
            var maxEndHour = schedulerDictionary.Max(i => i.Value?.WorkPeriod?.EndHour) ?? 24;
            var dayHours = new List<WeekHoursEntity>();

            // Loop day hours
            for (var hour = minStartHour; hour <= maxEndHour; hour++)
            {
                var slotScheduler = new List<int?>();

                // Loop day indexes
                for (var dayIndex = 0; dayIndex <= 6; dayIndex++)
                {
                    var currentDayInfo = schedulerDictionary.FirstOrDefault(i => i.Key == dayIndex).Value;
                    if (currentDayInfo != null)
                    {
                        var isBetweenRange1 = hour.IsBetween(currentDayInfo.WorkPeriod.StartHour, 
                                                             currentDayInfo.WorkPeriod.LunchStartHour - 1);
                        var isBetweenRange2 = hour.IsBetween(currentDayInfo.WorkPeriod.LunchEndHour, 
                                                             currentDayInfo.WorkPeriod.EndHour);

                        // Validate if it's a busy slot
                        if (currentDayInfo.BusySlots != null)
                        {
                            var isBusy = currentDayInfo.BusySlots.Any(busyHour => 
                                hour.IsBetween(busyHour.Start.Hour, busyHour.End.Hour));
                            if (isBusy)
                            {
                                slotScheduler.Add(null);
                                continue;
                            }
                        }

                        // If it's an available slot add slot, else add null.
                        if (isBetweenRange1 || isBetweenRange2)
                        {
                            slotScheduler.Add(hour);
                        }
                        else
                        {
                            slotScheduler.Add(null);
                        }
                    }
                    else
                    {
                        slotScheduler.Add(null);
                    }
                }

                dayHours.Add(new WeekHoursEntity
                {
                    Monday = slotScheduler[0],
                    Tuesday = slotScheduler[1],
                    Wednesday = slotScheduler[2],
                    Thursday = slotScheduler[3],
                    Friday = slotScheduler[4],
                    Saturday = slotScheduler[5],
                    Sunday = slotScheduler[6],
                });
            }

            return new SchedulerWeekEntity
            {
                Facility = schedulerEntity.Facility,
                SlotDurationMinutes = schedulerEntity.SlotDurationMinutes,
                WeekHours = dayHours
            };
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
