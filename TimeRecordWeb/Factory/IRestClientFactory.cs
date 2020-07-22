using RestSharp;

namespace TimeRecordWeb.Factory
{
    public interface IRestClientFactory
    {
        RestClient CreateClient();
        RestRequest CreateRequest(string endpoint, Method method);
    }
}