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

#if NETSTANDARD2_0
using Microsoft.Extensions.Logging;
#else
using ShipStation4Net.Logging;
#endif

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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShipStation4Net
{
    public abstract class ClientBase
    {
        public readonly Configuration Configuration;

        public static readonly JsonSerializerSettings SerializerSettings;

#if NETSTANDARD2_0
        ILogger ApiStateLogger = new LoggerFactory().CreateLogger("Api State");
#else
        ILog ApiStateLogger = LogProvider.For<ClientBase>();
#endif


        public event EventHandler<ShipStationResponseEventArgs> RequestCompleted;

        public RetryPolicy RetryPolicy = RetryPolicy.RetryOnApiLimit;

        protected string BaseUri { get; set; }

        static ClientBase()
        {
            //'DateTime Format and Time Zone' section of ShipStation documentation - ShipStation API operates in PST/PDT
            var timezoneString = "Pacific Standard Time";

#if NETSTANDARD2_0
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                timezoneString = "America/Los_Angeles";
            }
#endif

            var PacificTimezone = TimeZoneInfo.FindSystemTimeZoneById(timezoneString);

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
            if (resourceEndpoint == null)
            {
                resourceEndpoint = "";
            }

            var endpoint = (string.IsNullOrEmpty(resourceEndpoint)) ? BaseUri : string.Format("{0}/{1}", BaseUri, resourceEndpoint);

            // If you are supplying the filters yourself
            if (resourceEndpoint.StartsWith("?") || resourceEndpoint.Contains("/"))
            {
                endpoint = string.Format("{0}{1}", BaseUri, resourceEndpoint);
            }

            if (resourceEndpoint.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ||
                resourceEndpoint.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
            {
                endpoint = resourceEndpoint;
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
            IRestResponse<T> response;

            using (var client = new HttpClient())
            {
                //set basic authorization explicitly (which prevents additional extra call each time that ends up with 401 error)
                var byteArray = Encoding.UTF8.GetBytes($"{Configuration.UserName}:{Configuration.UserApiKey}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = Configuration.BaseUri;

                if (Configuration.PartnerId != null)
                {
                    client.DefaultRequestHeaders.Add("X-Partner", Configuration.PartnerId);
                }

                var httpResponse = await client.SendAsync(message).ConfigureAwait(false);
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
                            var apiLimitRemaining = int.Parse(httpResponse.Headers.GetValues("X-Rate-Limit-Remaining").FirstOrDefault());
                            var limitResetSeconds = int.Parse(httpResponse.Headers.GetValues("X-Rate-Limit-Reset").FirstOrDefault());
                            throw new ApiLimitReachedException(
                                $"{httpResponse.ReasonPhrase}\nThere are {limitResetSeconds} seconds remaning until a request reset.",
                                httpResponse,
                                apiLimitRemaining,
                                limitResetSeconds);
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
#if NETSTANDARD2_0
                ApiStateLogger.LogDebug(headerString + json);
#else
                ApiStateLogger.Log(LogLevel.Debug, () => headerString + json);
#endif
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

#if NETSTANDARD2_0
			ApiStateLogger.LogInformation($"There are {limitRemaining} requests left before a reset in {limitResetSeconds} seconds.");
#else
            ApiStateLogger.Log(LogLevel.Info, () => $"There are {limitRemaining} requests left before a reset in {limitResetSeconds} seconds.");
#endif
        }
    }
}
