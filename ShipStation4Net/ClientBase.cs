#region License
/*
 * Copyright 2017 Brandon James
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
#endregion

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShipStation4Net.Converters;
using ShipStation4Net.Events;
using ShipStation4Net.Exceptions;
using ShipStation4Net.FaultHandling;
using ShipStation4Net.Filters;
using ShipStation4Net.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ShipStation4Net
{
    public abstract class ClientBase
    {
        public readonly Configuration Configuration;

        public static readonly JsonSerializerSettings SerializerSettings;

        public int ApiLimitRemaining { get; set; }
        public int LimitResetSeconds { get; set; }

        public int ApiLimit
        {
            get { return Configuration.ApiLimit; }
            set { Configuration.ApiLimit = value; }
        }

        ILogger ApiStateLogger = new LoggerFactory().CreateLogger("Api State");

        public event EventHandler<ShipStationResponseEventArgs> RequestCompleted;

        public RetryPolicy RetryPolicy = RetryPolicy.RetryOnApiLimit;

        protected string BaseUri { get; set; }

        static ClientBase()
        {
            //'DateTime Format and Time Zone' section of ShipStation documentation - ShipStation API operates in PST/PDT
            TimeZoneInfo PacificTimezone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

            SerializerSettings = new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>()
                {
                    new SpecificTimeZoneDateConverter("yyyy-MM-dd HH:mm:ss", PacificTimezone)
                },
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public ClientBase(Configuration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            configuration.AreConfigurationSet();

            Configuration = configuration;
            LimitResetSeconds = 30;
            ApiLimitRemaining = Configuration.ApiLimit;
        }

        protected async Task<T> GetDataAsync<T>()
        {
            var response = await RetryPolicy.ExecuteAction(() =>
            {
                var message = new HttpRequestMessage(HttpMethod.Get, BaseUri);
                return ExecuteRequest<T>(message);
            }).ConfigureAwait(false);

            return response.Data;
        }

        protected Task<T> GetDataAsync<T>(IFilter filter)
        {
            return GetDataAsync<T>("", filter);
        }

        protected async Task<T> GetDataAsync<T>(string resourceEndpoint, IFilter filter = null)
        {
            var endpoint = (string.IsNullOrEmpty(resourceEndpoint)) ? BaseUri : string.Format("{0}/{1}", BaseUri, resourceEndpoint);
            // If you are supplying the filters yourself 
            if (resourceEndpoint.StartsWith("?") || resourceEndpoint.Contains("/"))
            {
                endpoint = string.Format("{0}{1}", BaseUri, resourceEndpoint);
            }

            var response = await RetryPolicy.ExecuteAction(() =>
            {
                var message = new HttpRequestMessage(HttpMethod.Get, endpoint);

                if (filter != null && !resourceEndpoint.Contains("?"))
                {
                    filter.AddFilter(message);
                }

                var uri = message.RequestUri.ToString();

                var resp = ExecuteRequest<T>(message);
                return resp;
            }).ConfigureAwait(false);

            return response.Data;
        }

        protected Task<T> GetDataAsync<T>(int id)
        {
            return GetDataAsync<T>(id.ToString(), null);
        }

        protected Task<T> PostDataAsync<T>(T data)
        {
            return PostDataAsync(BaseUri, data);
        }

        protected async Task<TResponse> PostDataAsync<TRequest, TResponse>(string resourceEndpoint, TRequest data)
        {
            var response = await RetryPolicy.ExecuteAction(() =>
            {
                var message = new HttpRequestMessage(HttpMethod.Post, string.Format("{0}/{1}", BaseUri, resourceEndpoint));
                var body = JsonConvert.SerializeObject(data, SerializerSettings);
                message.Content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
                return ExecuteRequest<TResponse>(message);
            }).ConfigureAwait(false);

            return response.Data;
        }

        protected Task<T> PostDataAsync<T>(string resourceEndpoint, T data)
        {
            return PostDataAsync<T, T>(resourceEndpoint, data);
        }

        protected Task<T> PutDataAsync<T>(int id, T data)
        {
            return PutDataAsync(string.Format("{0}/{1}", BaseUri, id), data);
        }

        protected async Task<T> PutDataAsync<T>(string resourceEndpoint, T data)
        {
            var response = await RetryPolicy.ExecuteAction(() =>
            {
                var message = new HttpRequestMessage(HttpMethod.Put, resourceEndpoint);
                message.Content = new StringContent(JsonConvert.SerializeObject(data, SerializerSettings));
                return ExecuteRequest<T>(message);
            }).ConfigureAwait(false);

            return response.Data;
        }

        protected Task<bool> DeleteDataAsync(int id)
        {
            return DeleteDataAsync(string.Format("{0}/{1}", BaseUri, id));
        }

        protected async Task<bool> DeleteDataAsync(string resourceEndpoint)
        {
            var response = await RetryPolicy.ExecuteAction(() =>
            {
                var message = new HttpRequestMessage(HttpMethod.Delete, resourceEndpoint);
                return ExecuteRequest<SuccessResponse>(message);
            }).ConfigureAwait(false);

            return response.Data.Success;
        }

        private async Task<IRestResponse<T>> ExecuteRequest<T>(HttpRequestMessage message)
        {
            var credentials = new NetworkCredential(Configuration.UserName, Configuration.UserApiKey);
            var handler = new HttpClientHandler { Credentials = credentials };
            IRestResponse<T> response;

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = Configuration.BaseUri;
                var httpResponse = await client.SendAsync(message).ConfigureAwait(false);

                if (Configuration.PartnerId != null)
                {
                    client.DefaultRequestHeaders.Add("X-Partner", Configuration.PartnerId);
                }

                ApiLimitRemaining = int.Parse(httpResponse.Headers.GetValues("X-Rate-Limit-Remaining").FirstOrDefault());
                LimitResetSeconds = int.Parse(httpResponse.Headers.GetValues("X-Rate-Limit-Reset").FirstOrDefault());
                ApiLimit = int.Parse(httpResponse.Headers.GetValues("X-Rate-Limit-Limit").FirstOrDefault());

                if (!httpResponse.IsSuccessStatusCode)
                {
                    switch (httpResponse.StatusCode)
                    {
                        case (HttpStatusCode)400:
                            throw new BadRequestException(httpResponse.ReasonPhrase, httpResponse);
                        case (HttpStatusCode)401:
                            throw new NotAuthorizedException(httpResponse.ReasonPhrase, httpResponse);
                        case (HttpStatusCode)404:
                            throw new NotFoundException(httpResponse.ReasonPhrase, httpResponse);
                        case (HttpStatusCode)405:
                            throw new MethodNotAllowedException(httpResponse.ReasonPhrase, httpResponse);
                        case (HttpStatusCode)429:
                            throw new ApiLimitReachedException(
                                $"{httpResponse.ReasonPhrase}\nThere are {LimitResetSeconds} seconds remaning until a request reset.",
                                httpResponse,
                                ApiLimitRemaining,
                                LimitResetSeconds);
                        case (HttpStatusCode)500:
                            throw new InternalServerErrorException(httpResponse.ReasonPhrase, httpResponse);
                        default:
                            throw new ShipStationException($"This error is not handled\n{httpResponse.ReasonPhrase}");
                    }
                }

                PrintApiLimitDetails(httpResponse);
                var headerString = "";

                foreach (var header in httpResponse.Headers)
                {
                    headerString += $"{header.Key}: {header.Value}\n";
                }

                var json = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                ApiStateLogger.LogDebug(headerString + json);
                T responseData = JsonConvert.DeserializeObject<T>(json, SerializerSettings);

                response = new RestResponse<T>
                {
                    Data = responseData,
                    Request = message,
                    Response = httpResponse
                };

                OnRequestCompleted(response.Request, response.Response);
            }

            return response;
        }

        protected virtual void OnRequestCompleted(HttpRequestMessage request, HttpResponseMessage response)
        {
            var responseEventArgs = new ShipStationResponseEventArgs { Request = request, Response = response };

            RequestCompleted?.Invoke(this, responseEventArgs);
        }

        private void PrintApiLimitDetails(HttpResponseMessage response)
        {
            string limitRemaining = response.Headers.GetValues("X-Rate-Limit-Remaining").FirstOrDefault();
            string limitResetSeconds = response.Headers.GetValues("X-Rate-Limit-Reset").FirstOrDefault();

            ApiStateLogger.LogInformation($"There are {limitRemaining} requests left before a reset in {limitResetSeconds} seconds.");
        }
    }
}
