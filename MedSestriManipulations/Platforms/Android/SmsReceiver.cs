using Android.App;
using Android.Content;
using Android.OS;
using MedSestriManipulations.Services.SMS;

namespace MedSestriManipulations.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
    public class SmsReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action != "android.provider.Telephony.SMS_RECEIVED")
                return;

            var bundle = intent.Extras;
            if (bundle == null) return;

            Java.Lang.Object[] pdus = (Java.Lang.Object[])bundle.Get("pdus");
            if (pdus == null) return;

            foreach (var pdu in pdus)
            {
                global::Android.Telephony.SmsMessage message;
                if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                {
                    string format = bundle.GetString("format");
                    message = global::Android.Telephony.SmsMessage.CreateFromPdu((byte[])pdu, format);
                }
                else
                {
                    message = global::Android.Telephony.SmsMessage.CreateFromPdu((byte[])pdu);
                }

                string body = message?.MessageBody;
                string sender = message?.OriginatingAddress;

                if (!string.IsNullOrEmpty(body))
                {
                    var parserService = MauiProgram.AppInstance.Services.GetService<SmsParserService>();
                    _ = parserService?.HandleSmsAsync(body, sender); // асинхронно
                }
            }
        }
    }

}
