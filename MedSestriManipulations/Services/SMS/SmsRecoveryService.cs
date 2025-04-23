using static SmsParserService;

namespace MedSestriManipulations.Services.SMS
{
    public static class SmsRecoveryService
    {
        public static async Task RecoverAsync()
        {
#if ANDROID
            await ReadMissedSmsSinceAsync();
#else
            await Task.CompletedTask;
#endif
        }

#if ANDROID
        public static async Task ReadMissedSmsSinceAsync()
        {
            try
            {
                var contentResolver = Android.App.Application.Context.ContentResolver;
                var uri = Android.Net.Uri.Parse("content://sms/inbox");

                var projection = new[] { "_id", "address", "date", "body" };

                var since = LastSmsReadTracker.GetLastReadTime().AddMinutes(-10);
                var unixSince = new DateTimeOffset(since).ToUnixTimeMilliseconds();
                var selection = $"date >= {unixSince}";
                //var selection = $"date >= {unixSince} AND (address = '1917' OR address = '+359882259007')";

                var cursor = contentResolver.Query(uri, projection, selection, null, "date ASC");

                System.Diagnostics.Debug.WriteLine("[SMS Recovery] Стартиране...");
                if (cursor == null)
                {
                    System.Diagnostics.Debug.WriteLine("[SMS Recovery] Курсорът е null – вероятно нямаш разрешение.");
                    return;
                }

                if (cursor != null && cursor.MoveToFirst())
                {
                    do
                    {
                        var body = cursor.GetString(cursor.GetColumnIndexOrThrow("body"));
                        var sender = cursor.GetString(cursor.GetColumnIndexOrThrow("address"));
                        var dateMillis = cursor.GetLong(cursor.GetColumnIndexOrThrow("date"));
                        var smsDate = DateTimeOffset.FromUnixTimeMilliseconds(dateMillis).DateTime;

                        OnSmsReceived(body, sender);
                        LastSmsReadTracker.SaveLastReadTime(smsDate);

                    } while (cursor.MoveToNext());

                    cursor.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SMS Recovery] Error: {ex.Message}");
            }
        }
#endif
    }
}
