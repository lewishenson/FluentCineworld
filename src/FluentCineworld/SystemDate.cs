using System;

namespace FluentCineworld
{
    public static class SystemDate
    {
        public static Func<DateTime> UtcNow = () => DateTime.UtcNow;
    }
}
