using System;

namespace DoctorScheduler.CrossCutting.Exceptions
{
    [Serializable]
    public class SchedulerServiceException : Exception
    {
        public SchedulerServiceException()
        {
        }

        public SchedulerServiceException(string message)
            : base(message)
        {
        }

        public SchedulerServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
