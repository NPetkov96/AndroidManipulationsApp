namespace MedSestriManipulations.Services.SMS
{
    public class SmsPermissionService
    {
        public async Task<bool> EnsureSmsPermissionAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Sms>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Sms>();
            }

            return status == PermissionStatus.Granted;
        }
    }
}
