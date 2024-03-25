using Serilog;

namespace LogcatHttpClient
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Serilog.Log.Logger = new LoggerConfiguration()
                .WriteTo.File($"{System.Environment.SpecialFolder.Personal}/logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}