using System;

namespace DoctorScheduler.CrossCutting.Extensions
{
    public static class IntegerExtension
    {
        public static bool IsBetween(this DateTime? x, DateTime? min, DateTime? max)
        {
            return x >= min && x < max;
        }
    }
}
