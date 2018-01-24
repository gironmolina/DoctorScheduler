using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.Tests.Builders
{
    public class FacilityDtoBuilder
    {
        private readonly FacilityDto innerObject = new FacilityDto();

        public FacilityDtoBuilder WithDefaultValues()
        {
            return this
                .FacilityId("")
                .Name("")
                .Address("");
        }

        public FacilityDtoBuilder FacilityId(string facilityId)
        {
            this.innerObject.FacilityId = facilityId;
            return this;
        }

        public FacilityDtoBuilder Name(string name)
        {
            this.innerObject.Name = name;
            return this;
        }

        public FacilityDtoBuilder Address(string address)
        {
            this.innerObject.Address = address;
            return this;
        }

        public FacilityDto Build()
        {
            return this.innerObject;
        }
    }
}
