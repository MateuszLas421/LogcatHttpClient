using Android.Views;
using LogcatHttpClient.HttpClients.Refit;
using LogcatHttpClient.HttpClients.StandardHttpClient;
using LogcatHttpClient.Tools;
using Refit;
using Serilog;
using System.Diagnostics;

namespace LogcatHttpClient.Activities;

[Activity]
public class ActivityHttpClientTest : Android.App.Activity
{
    private TextView logTextView;
    private ScrollView scrollView;
    private Button start;
    private Button end;
    private IApi api;
    private bool isStarted = false;
    private string url = ""; ///  URL
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        SetContentView(Resource.Layout.activity_httpclienttest);

        logTextView = FindViewById<TextView>(Resource.Id.logsView);
        scrollView = FindViewById<ScrollView>(Resource.Id.scrollView);
        start = FindViewById<Button>(Resource.Id.start);
        end = FindViewById<Button>(Resource.Id.end);

        api = RestService.For<IApi>(url);
        start.Click += Start_Click;
        end.Click += End_Click;
    }

    private void End_Click(object? sender, EventArgs e)
    {
        Logcat();
        SendMail();
    }

    private void Start_Click(object? sender, EventArgs e)
    {
        if (isStarted)
            return;
        logTextView.Text = string.Empty;
        isStarted = true;
        Write(" Start ");
        CheckInternet();
        CheckPing();
        StartRefit();
        StartStandard();
        Write(" End ");
        isStarted = false;
    }

    private void CheckPing()
    {
        var ping = new PingTool();
        Write(ping.Ping(url));
    }

    private void CheckInternet()
    {
        if (new Internet().IsInternetAvailable())
        {
            Write(" InternetStatus OK ");
        }
        else
        {
            Write(" InternetStatus Error ");
        }
    }

    private void StartRefit()
    {
        try
        {
            Write(" StartRefit ");
            api.Test(new Input() {  test = "1234" })
                .Subscribe(result =>
                {
                    if (!string.IsNullOrEmpty(result?.result))
                        Write(" Refit Success ");
                },
                ex =>
                {
                    Write($" Refit Error => {ex.Message} {ex?.Source} {ex?.InnerException?.Message}");
                });
        }
        catch (Exception ex)
        {
            Write($" Refit Error => {ex.Message} {ex?.Source} {ex?.InnerException?.Message}");
        }
    }

    private async void StartStandard()
    {
        try
        {
            Write(" Start StandardHttpClient");
            var client = new StandardHttpClient(url);
            var result = await client.SendAuthRequest(new Input() {  test = "1234" });
            Write(result);
        }
        catch (Exception ex)
        {
            Write($" Standard Error => {ex.Message} {ex?.Source} {ex?.InnerException?.Message}");
        }
    }

    private void Write(string text)
    {
        RunOnUiThread(() =>
        {
            logTextView.Text += $"\n{DateTime.UtcNow} {text}";
            scrollView.FullScroll(FocusSearchDirection.Down);
        });
        Log.Information(text);
    }

    private void SendMail()
    {
        var email = new Email();
        logTextView.Text = email.Send(this, "", logTextView?.Text);
    }
    private void Logcat()
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
            logTextView.Text += "\n\n";
            logTextView.Text += logOutput;
        });

        process.WaitForExit();
        process.Close();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}