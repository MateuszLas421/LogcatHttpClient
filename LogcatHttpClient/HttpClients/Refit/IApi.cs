using Refit;

namespace LogcatHttpClient.HttpClients.Refit
{
    public class Input
    {
        public string test { get; set; }
    }
    public class ApiResult
    {
        public string result { get; set; }
    }

    public interface IApi
    {
        [Post("/test")]
        IObservable<ApiResult> Test([Body] Input input);
    }
}
