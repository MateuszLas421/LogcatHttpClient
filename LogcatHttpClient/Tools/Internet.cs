using Microsoft.Maui.Networking;
namespace LogcatHttpClient.Tools;

public class Internet
{
    public bool IsInternetAvailable()
    {
        try
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
