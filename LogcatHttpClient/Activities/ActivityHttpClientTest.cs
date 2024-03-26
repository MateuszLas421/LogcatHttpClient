using System.Diagnostics;

namespace LogcatHttpClient.Activities;

[Activity(Label = "ActivityHttpClientTest")]
public class ActivityHttpClientTest : Android.App.Activity
{
    private TextView logTextView;
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        SetContentView(Resource.Layout.activity_httpclienttest);

        logTextView = FindViewById<TextView>(Resource.Id.logsVIew);

        var logThread = new System.Threading.Thread(ReadLogcatInBackground);
        logThread.Start();
    }

    private void ReadLogcatInBackground()
    {
        var process = new Process();
        process.StartInfo.FileName = "logcat";
        process.StartInfo.Arguments = "-d"; 
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;

        process.Start();

        string logOutput = process.StandardOutput.ReadToEnd();

        RunOnUiThread(() =>
        {
            logTextView.Text = logOutput;
        });

        process.WaitForExit();
        process.Close();
    }
}