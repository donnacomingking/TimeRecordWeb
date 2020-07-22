using Microsoft.AspNetCore.Http;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TimeRecordWeb.Factory;
using TimeRecordWeb.Models;

namespace TimeRecordWeb.Helpers
{
    public class TimeRecordAPIClient : ITimeRecordAPIClient
    {
        private IUserAPIClient _userAPIClient;
        private IRestClientFactory _restClientFactory;
        private RestClient _restClient;
        private string _apiToken;
        HttpContext _httpContext;

        public TimeRecordAPIClient(IRestClientFactory restClientFactory, IUserAPIClient userAPIClient, IHttpContextAccessor httpContextAccessor)
        {
            _restClientFactory = restClientFactory;
            _restClient = _restClientFactory.CreateClient();
            _userAPIClient = userAPIClient;
            _httpContext = httpContextAccessor.HttpContext;
            _apiToken = GetToken();          
        }

        private string GetToken()
        {
            var token = string.Empty;
            var claim = _httpContext.User.Claims.Where(x => x.Type == "AcessToken");

            foreach (var item in claim)
            {
                if (item.GetType().GetProperties().Any(prop => prop.Name == "Value"))
                {
                    token = item.GetType().GetProperty("Value").GetValue(item, null).ToString();
                }
            }

            return token;
        }

        public async Task<IList<TimeRecordModel>> GetAllTimeRecordAsync()
        {
            var restRequest = _restClientFactory.CreateRequest("api/TimeRecord/GetAllTimeRecord", Method.GET);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("authorization", string.Format("Bearer {0}", _apiToken));

            var response = await _restClient.ExecuteAsync<IList<TimeRecordModel>>(restRequest);

            if (response.Data == null)
                throw new Exception(response.ErrorMessage);

            return response.Data;
        }

        public async Task<IList<TimeRecordModel>> GetAllTimeRecordByUserId(int userId)
        {
            var restRequest = _restClientFactory.CreateRequest(string.Format("api/TimeRecord/GetAllTimeRecordByUserId/{0}", userId), Method.GET);
            restRequest.RequestFormat = DataFormat.Json; 
            restRequest.AddHeader("authorization", string.Format("Bearer {0}", _apiToken));

            var response = await _restClient.ExecuteAsync<IList<TimeRecordModel>>(restRequest);

            if (response.Data == null)
                throw new Exception(response.ErrorMessage);

            return response.Data;
        }

        public async Task<TimeRecordModel> GetTimeRecordById(int id)
        {
            var restRequest = _restClientFactory.CreateRequest(string.Format("api/TimeRecord/GetTimeRecordById/{0}", id), Method.GET);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("authorization", string.Format("Bearer {0}", _apiToken));

            var response = await _restClient.ExecuteAsync<TimeRecordModel>(restRequest);

            if (response.Data == null)
                throw new Exception(response.ErrorMessage);

            return response.Data;
        }

        public async Task<TimeRecordModel> CreateTimeRecord(TimeRecordModel timeRecordEntry)
        {
            var restRequest = _restClientFactory.CreateRequest("api/TimeRecord/CreateTimeRecord", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddJsonBody(timeRecordEntry); 
            restRequest.AddHeader("authorization", string.Format("Bearer {0}", _apiToken));
            var result = new TimeRecordModel();
            var exceptions = new List<Exception>();
            var remainingTries = 5;

            do
            {
                --remainingTries;
                try
                {
                    var response = await _restClient.ExecuteAsync<TimeRecordModel>(restRequest);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        result = response.Data;
                        return result;
                    }                       
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }
            while (remainingTries > 0);

            return result;
        }

        public async Task<TimeRecordModel> UpdateTimeRecord(int id, TimeRecordModel timeRecordEntry)
        {
            var restRequest = _restClientFactory.CreateRequest(string.Format("api/TimeRecord/UpdateTimeRecord/{0}", id), Method.PUT);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddJsonBody(timeRecordEntry);
            restRequest.AddHeader("authorization", string.Format("Bearer {0}", _apiToken));
            var result = new TimeRecordModel();
            var exceptions = new List<Exception>();
            var remainingTries = 5;

            do
            {
                --remainingTries;
                try
                {
                    var response = await _restClient.ExecuteAsync<TimeRecordModel>(restRequest);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        result = response.Data;
                        return result;
                    }
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }
            while (remainingTries > 0);

            return result;
        }

        public async Task<bool> DeleteTimeRecordById(int id)
        {
            var restRequest = _restClientFactory.CreateRequest(string.Format("api/TimeRecord/DeleteTimeRecordById/{0}", id), Method.DELETE);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("authorization", string.Format("Bearer {0}", _apiToken));

            var response = await _restClient.ExecuteAsync<bool>(restRequest);

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception(response.ErrorMessage);

            return response.Data;
        }
    }
}
