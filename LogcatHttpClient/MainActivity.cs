using Android.Content;
using Android.Content.PM;
using LogcatHttpClient.Activities;
using Microsoft.Maui.ApplicationModel;

namespace LogcatHttpClient
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button httpclient;
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            /*            Serilog.Log.Logger = new LoggerConfiguration()
                            .WriteTo.AndroidLog()
                            .WriteTo.File( Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "logs.txt"), rollingInterval: RollingInterval.Day)
                            .CreateLogger();*/


            httpclient = FindViewById<Button>(Resource.Id.logcatHttpClientTest);
            httpclient.Click += Httpclient_Click;
        }

        private void Httpclient_Click(object? sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(ActivityHttpClientTest)));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}