using System;

namespace DoctorScheduler.CrossCutting.Extensions
{
    public static class IntegerExtension
    {
        public static bool IsBetween(this TimeSpan? x, TimeSpan? min, TimeSpan? max)
        {
            return x >= min && x < max;
        }
    }
}
