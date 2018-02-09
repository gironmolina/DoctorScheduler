using DoctorScheduler.Entities;

namespace DoctorScheduler.TestUtils.Builders.Entities
{
    public class WorkPeriodEntityBuilder
    {
        private readonly WorkPeriodEntity innerObject = new WorkPeriodEntity();

        public WorkPeriodEntityBuilder WithDefaultValues()
        {
            return this.StartHour(9)
                .EndHour(17)
                .LunchStartHour(13)
                .LunchEndHour(14);
        }

        public WorkPeriodEntityBuilder StartHour(int startHour)
        {
            this.innerObject.StartHour = startHour;
            return this;
        }

        public WorkPeriodEntityBuilder EndHour(int endHour)
        {
            this.innerObject.EndHour = endHour;
            return this;
        }

        public WorkPeriodEntityBuilder LunchStartHour(int lunchStartHour)
        {
            this.innerObject.LunchStartHour = lunchStartHour;
            return this;
        }

        public WorkPeriodEntityBuilder LunchEndHour(int lunchEndHour)
        {
            this.innerObject.LunchEndHour = lunchEndHour;
            return this;
        }

        public WorkPeriodEntity Build()
        {
            return this.innerObject;
        }
    }
}
