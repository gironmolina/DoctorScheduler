namespace DoctorScheduler.CrossCutting.Extensions
{
    public static class IntegerExtension
    {
        public static bool IsBetween(this int x, int? min, int? max)
        {
            return x >= min && x <= max;
        }
    }
}
