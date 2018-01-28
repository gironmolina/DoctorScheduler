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

        public async Task<SchedulerEntity> GetWeeklyAvailability(string date)
        {
            var url = $"{SchedulerUrl}/GetWeeklyAvailability/{date}";
            Logger.DebugFormat("Getting weekly availability for: {0}", date);
            var schedulerEntity = await HttpClientHelpers.GetAsync<SchedulerEntity>(url).ConfigureAwait(false);

            NewMethod(schedulerEntity);

            return schedulerEntity;
        }

        private void NewMethod(SchedulerEntity schedulerEntity)
        {
            var slotDictionary = new Dictionary<int, SlotEntity>
            {
                {0, schedulerEntity.Monday},
                {1, schedulerEntity.Tuesday},
                {2, schedulerEntity.Wednesday},
                {3, schedulerEntity.Thursday},
                {4, schedulerEntity.Friday},
                {5, schedulerEntity.Saturday},
                {6, schedulerEntity.Sunday},
            };

            var min = slotDictionary.Min(i => i.Value?.WorkPeriod?.StartHour) ?? 0;
            var max = slotDictionary.Max(i => i.Value?.WorkPeriod?.EndHour) ?? 24;
            var dayHours = new List<DayHours>();

            // TODO Validate null
            for (var i = min; i <= max; i++)
            {
                var hour = new List<int?>();
                for (var j = 0; j <= 6; j++)
                {
                    var day = slotDictionary.FirstOrDefault(t => t.Key == j).Value;
                    if (day != null)
                    {
                        var isBetweenRange1 = this.Between(i, day.WorkPeriod.StartHour, day.WorkPeriod.LunchStartHour - 1);
                        var isBetweenRange2 = this.Between(i, day.WorkPeriod.LunchEndHour, day.WorkPeriod.EndHour);

                        if (day.BusySlots != null)
                        {
                            var isBusy = day.BusySlots.Any(e => this.Between(i, e.Start.Hour, e.End.Hour));
                            if (isBusy)
                            {
                                hour.Add(null);
                                continue;
                            }
                        }

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

                dayHours.Add(new DayHours
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
        }

        public async Task<bool> TakeSlot(TakeSlotEntity slot)
        {
            var url = $"{SchedulerUrl}/TakeSlot";
            Logger.DebugFormat("Taking slot: {0}", JsonConvert.SerializeObject(slot));
            return await HttpClientHelpers.PostAsync(url, slot).ConfigureAwait(false);
        }

        private bool Between(int? x, int? min, int? max)
        {
            return x >= min &&  x <= max;
        }
    }

    public class DayHours
    {
        public int? Monday { get; set; }

        public int? Tuesday { get; set; }

        public int? Wednesday { get; set; }

        public int? Thursday { get; set; }

        public int? Friday { get; set; }

        public int? Saturday { get; set; }

        public int? Sunday { get; set; }
    }
}
