using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using TimeRecordWeb.Helpers;

namespace TimeRecordWeb.Factory
{
    public class RestClientFactory : IRestClientFactory
    {
        private readonly IConfiguration _config;
        private string _apiBaseUrl;

        public RestClientFactory(IConfiguration config)
        {
            _config = config;
            GetApiBaseUrl();
        }

        private void GetApiBaseUrl()
        {
            var appSettingsSection = _config.GetSection("AppSettings");
            var webApiUrl = appSettingsSection.Get<AppSettings>();
            _apiBaseUrl = webApiUrl.WebApiUrl;
        }

        public RestClient CreateClient()
        {
            var client = new RestClient(_apiBaseUrl);
            client.UseNewtonsoftJson();
            return client;
        }

        public RestRequest CreateRequest(string endpoint, Method method)
        {
            return new RestRequest(string.Format("{0}/{1}", _apiBaseUrl, endpoint), method);
        }
    }
}