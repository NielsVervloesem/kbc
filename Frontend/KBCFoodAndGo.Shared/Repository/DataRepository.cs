using KBCFoodAndGo.Shared.Exceptions;
using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Models;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Shared.Repository
{
    public class DataRepository : IDataRepository
    {
        public async Task<T> OnGetAsync<T>(string uri, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);
                string jsonResult = string.Empty;
                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.GetAsync(uri));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException();
                }

                throw new HttpRequestExceptionException(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<T> OnPostAsyncReturnsListOfMeals<T>(string uri, List<long> id, string authToken="")
        {
                try
                {
                    HttpClient httpClient = CreateHttpClient(uri);

                    var content = new StringContent(JsonConvert.SerializeObject(id));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    string jsonResult = string.Empty;

                    var responseMessage = await Policy
                        .Handle<WebException>(ex =>
                        {
                            Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                            return true;
                        })
                        .WaitAndRetryAsync
                        (
                            5,
                            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                        )
                        .ExecuteAsync(async () => await httpClient.PostAsync(uri, content));

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var json = JsonConvert.DeserializeObject<T>(jsonResult);
                        return json;
                    }

                    if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                        responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new ServiceAuthenticationException(jsonResult);
                    }

                    throw new HttpRequestExceptionException(responseMessage.StatusCode, jsonResult);

                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                    throw;
                }
            }
        
        public async Task<T> OnPostAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);
                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                string jsonResult = string.Empty;
                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException();
                }

                if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new BadRequestException();
                }

                throw new HttpRequestExceptionException(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<T> OnPutAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException();
                }

                throw new HttpRequestExceptionException(responseMessage.StatusCode, jsonResult);
                

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }


        public async Task<User> OnPutAsyncReturnUser(string uri, List<Meal> data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<User>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException();
                }

                throw new HttpRequestExceptionException(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task OnDeleteAsync(string uri, string authToken = "")
        {
            HttpClient httpClient = CreateHttpClient(authToken);
            await httpClient.DeleteAsync(uri);
        }

        private HttpClient CreateHttpClient(string authToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }
            return httpClient;
        }
    }
}
