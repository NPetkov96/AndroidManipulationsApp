using System.Globalization;

namespace MedSestriManipulations.Services.SMS
{
    public static class LastSmsReadTracker
    {
        private const string Key = "LastSmsReadDate";

        public static void SaveLastReadTime(DateTime time)
        {
            Preferences.Set(Key, time.ToUniversalTime().ToString("O"));
        }

        public static DateTime GetLastReadTime()
        {
            var value = Preferences.Get(Key, null);
            return DateTime.TryParse(value, null, DateTimeStyles.RoundtripKind, out var result)
                ? result.ToLocalTime()
                : DateTime.Now.AddDays(-1);
        }
    }
}
