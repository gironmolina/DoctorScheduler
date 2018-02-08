using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.TestUtils.Builders.Dtos
{
    public class PatientDtoBuilder
    {
        private readonly PatientDto innerObject = new PatientDto();

        public PatientDtoBuilder WithDefaultValues()
        {
            return this
                .Name("Mario")
                .SecondName("Neta")
                .Email("mario@myspace.es")
                .Phone("555 44 33 22");
        }

        public PatientDtoBuilder Name(string name)
        {
            this.innerObject.Name = name;
            return this;
        }

        public PatientDtoBuilder SecondName(string secondName)
        {
            this.innerObject.SecondName = secondName;
            return this;
        }

        public PatientDtoBuilder Email(string email)
        {
            this.innerObject.Email = email;
            return this;
        }

        public PatientDtoBuilder Phone(string phone)
        {
            this.innerObject.Phone = phone;
            return this;
        }

        public PatientDto Build()
        {
            return this.innerObject;
        }
    }
}