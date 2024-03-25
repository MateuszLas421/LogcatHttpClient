using System.Net.NetworkInformation;

namespace LogcatHttpClient.Tools;

public class PingTool
{ 
    public string Ping(string url)
    {
        Ping ping = new();
        try
        {
            PingReply reply = ping.Send(url, 3000);
            if (reply.Status == IPStatus.Success)
            {
                 return $"Ping successful.";
            }
            else
            {
                return $"Ping failed: {reply.Status.ToString()}";
            }
        }
        catch (PingException ex)
        {
            return $"Ping failed: {ex.Message}";
        }
    }
}
