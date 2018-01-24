using System;

namespace DoctorScheduler.Infrastucture.Exceptions
{
    [Serializable]
    public class SchedulerBadRequestException : Exception
    {
        public SchedulerBadRequestException()
        {
        }
        
        public SchedulerBadRequestException(string message)
            : base(message)
        {
        }
        
        public SchedulerBadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}