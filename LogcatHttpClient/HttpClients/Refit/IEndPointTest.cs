namespace LogcatHttpClient.HttpClients.Refit
{
    public interface IEndPointTest
    {
        IObservable<HttpResponseMessage> GetGoogle();
    }
}
