using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.TestUtils.Builders.Dtos
{
    public class FacilityDtoBuilder
    {
        private readonly FacilityDto innerObject = new FacilityDto();

        public FacilityDtoBuilder WithDefaultValues()
        {
            return this
                .FacilityId("e9f7bd81-965d-4464-b607-999112b56022")
                .Name("Las Palmeras")
                .Address("Plaza de la independencia 36, 38006 Santa Cruz de Tenerife");
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
