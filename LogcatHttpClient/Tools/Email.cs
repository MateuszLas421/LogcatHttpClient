using Android.Content;
using Android.Content.PM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LogcatHttpClient.Tools;

internal class Email
{
    public string Send(Activity activity, string emailAddress, string message) 
    {
        try
        {
            Intent emailIntent = new Intent(Intent.ActionSend);
            emailIntent.SetType("message/rfc822");
            emailIntent.PutExtra(Intent.ExtraEmail, new string[] { emailAddress });
            emailIntent.PutExtra(Intent.ExtraSubject, "Status");
            emailIntent.PutExtra(Intent.ExtraText, message);

            if (emailIntent.ResolveActivity(activity.PackageManager) != null)
            {
                activity.StartActivity(emailIntent);
                return "ok";
            }
            else
            {
                return "Not Found email App";
            }
        }
        catch (Exception ex)
        {
            return $"Exception : {ex.Message}";
        }
    }
}
