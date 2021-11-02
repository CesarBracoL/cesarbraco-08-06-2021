using Domain.Common;
using System;

namespace Domain
{
    public static class Helpers
    {
        public static DateTime GetDateTimeUtcLocal()
        {
            return DateTime.UtcNow.AddHours(Constants.UtcLocal);
        }
    }
}
