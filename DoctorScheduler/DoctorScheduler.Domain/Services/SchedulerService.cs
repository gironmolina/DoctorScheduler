using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using DoctorScheduler.Entities;
using DoctorScheduler.Infrastucture.Helpers;
using log4net;
using Newtonsoft.Json;

namespace DoctorScheduler.Domain.Services
{
    public class SchedulerService : ISchedulerService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SchedulerService));
        private static readonly string SchedulerUrl = ConfigurationManager.AppSettings["SchedulerAPIUrl"];

        public async Task<SchedulerWeekEntity> GetWeeklyAvailability(string date)
        {
            var url = $"{SchedulerUrl}/GetWeeklyAvailability/{date}";
            Logger.DebugFormat("Getting weekly availability for: {0}", date);
            var schedulerEntity = await HttpClientHelpers.GetAsync<SchedulerEntity>(url).ConfigureAwait(false);
            return GetSchedulerWeek(schedulerEntity);
        }

        private SchedulerWeekEntity GetSchedulerWeek(SchedulerEntity schedulerEntity)
        {
            var schedulerDictionary = GetWeekDictionary(schedulerEntity);
            var minStartHour = schedulerDictionary.Min(i => i.Value?.WorkPeriod?.StartHour) ?? 0;
            var maxEndHour = schedulerDictionary.Max(i => i.Value?.WorkPeriod?.EndHour) ?? 24;
            var dayHours = new List<WeekHoursEntity>();

            // Loop day hours
            for (var i = minStartHour; i <= maxEndHour; i++)
            {
                var hour = new List<int?>();

                // Loop day indexes
                for (var dayIndex = 0; dayIndex <= 6; dayIndex++)
                {
                    var day = schedulerDictionary.FirstOrDefault(d => d.Key == dayIndex).Value;
                    if (day != null)
                    {
                        var isBetweenRange1 = this.IsBetween(i, day.WorkPeriod.StartHour, day.WorkPeriod.LunchStartHour - 1);
                        var isBetweenRange2 = this.IsBetween(i, day.WorkPeriod.LunchEndHour, day.WorkPeriod.EndHour);
                        
                        // Validate if it's a busy slot
                        if (day.BusySlots != null)
                        {
                            var isBusy = day.BusySlots.Any(e => this.IsBetween(i, e.Start.Hour, e.End.Hour));
                            if (isBusy)
                            {
                                hour.Add(null);
                                continue;
                            }
                        }

                        // If it's an available slot add hour, else add null.
                        if (isBetweenRange1 || isBetweenRange2)
                        {
                            hour.Add(i);
                        }
                        else
                        {
                            hour.Add(null);
                        }
                    }
                    else
                    {
                        hour.Add(null);
                    }
                }

                dayHours.Add(new WeekHoursEntity
                {
                    Monday = hour[0],
                    Tuesday = hour[1],
                    Wednesday = hour[2],
                    Thursday = hour[3],
                    Friday = hour[4],
                    Saturday = hour[5],
                    Sunday = hour[6],
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

        public async Task<bool> TakeSlot(TakeSlotEntity slot)
        {
            var url = $"{SchedulerUrl}/TakeSlot";
            Logger.DebugFormat("Taking slot: {0}", JsonConvert.SerializeObject(slot));
            return await HttpClientHelpers.PostAsync(url, slot).ConfigureAwait(false);
        }

        private bool IsBetween(int? x, int? min, int? max)
        {
            return x >= min &&  x <= max;
        }
    }
}
