using System;
using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.TestUtils.Builders.Dtos
{
    public class TakeSlotDtoBuilder
    {
        private readonly TakeSlotDto innerObject = new TakeSlotDto();

        public TakeSlotDtoBuilder WithDefaultValues()
        {
            return this
                .FacilityId("e9f7bd81-965d-4464-b607-999112b56022")
                .Start(new DateTime(2017, 06, 13, 11, 0, 0))
                .End(new DateTime(2017, 06, 13, 12, 0, 0))
                .Patient(new PatientDtoBuilder()
                    .WithDefaultValues().Build())
                .Comments("my arm hurts a lot");
        }

        public TakeSlotDtoBuilder FacilityId(string facilityId)
        {
            this.innerObject.FacilityId = facilityId;
            return this;
        }

        public TakeSlotDtoBuilder Start(DateTime start)
        {
            this.innerObject.Start = start;
            return this;
        }

        public TakeSlotDtoBuilder End(DateTime end)
        {
            this.innerObject.End = end;
            return this;
        }

        public TakeSlotDtoBuilder Patient(PatientDto patient)
        {
            this.innerObject.Patient = patient;
            return this;
        }

        public TakeSlotDtoBuilder Comments(string comments)
        {
            this.innerObject.Comments = comments;
            return this;
        }

        public TakeSlotDto Build()
        {
            return this.innerObject;
        }
    }
}