using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.Tests.Builders
{
    public class WorkPeriodDtoBuilder
    {
        private readonly WorkPeriodDto innerObject = new WorkPeriodDto();

        public WorkPeriodDtoBuilder WithDefaultValues()
        {
            return this
                .StartHour(9)
                .EndHour(17)
                .LunchStartHour(13)
                .LunchEndHour(14);
        }

        public WorkPeriodDtoBuilder StartHour(int startHour)
        {
            this.innerObject.StartHour = startHour;
            return this;
        }

        public WorkPeriodDtoBuilder EndHour(int endHour)
        {
            this.innerObject.EndHour = endHour;
            return this;

        }

        public WorkPeriodDtoBuilder LunchStartHour(int lunchStartHour)
        {
            this.innerObject.LunchStartHour = lunchStartHour;
            return this;
        }

        public WorkPeriodDtoBuilder LunchEndHour(int lunchEndHour)
        {
            this.innerObject.LunchEndHour = lunchEndHour;
            return this;
        }

        public WorkPeriodDto Build()
        {
            return this.innerObject;
        }
    }
}
