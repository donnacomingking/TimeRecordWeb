using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using System;
using System.Net;
using TimeRecordWeb.Factory;
using TimeRecordWeb.Models;

namespace TimeRecordWeb.Helpers
{
    public class UserAPIClient : IUserAPIClient
    {
        private IRestClientFactory _restClientFactory;
        private RestClient _restClient;

        public UserAPIClient(IRestClientFactory restClientFactory)
        {
            _restClientFactory = restClientFactory;
            _restClient = _restClientFactory.CreateClient();
        }

        public string GetToken(User user)
        {
            var tokenModel = AuthenticateUserFromAPI(user);
            var token = tokenModel.Token;

            return token;
        }

        private User AuthenticateUserFromAPI(User user)
        {
            var restRequest = _restClientFactory.CreateRequest("api/User/authenticate", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddJsonBody(user);

            var response = _restClient.Execute<User>(restRequest);

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception(response.ErrorMessage);

            return response.Data;
        }
    }
}
