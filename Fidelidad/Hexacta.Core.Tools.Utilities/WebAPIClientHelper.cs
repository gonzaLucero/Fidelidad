using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Hexacta.Core.Tools.Utilities
{
    public class WebAPIClientHelper
    {
        HttpClient client = new HttpClient();

        public WebAPIClientHelper(string BaseAddress)
        {
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        List<MediaTypeFormatter> formatters = new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter(), new XmlMediaTypeFormatter() };


        public async Task<T> GetAsync<T>(string path) where T : class
        {
            return await GetAsync<T>(path, token: null, useBearerToken: false);
        }
        public async Task<T> GetAsync<T>(string path, string token, bool useBearerToken = true) where T : class
        {
            T responseObject = null;

            if (useBearerToken && !client.DefaultRequestHeaders.Contains("Authorization"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            HttpResponseMessage response = await GetAsync(path, token, useBearerToken);
            if (response.IsSuccessStatusCode)
            {
                responseObject = await response.Content.ReadAsAsync<T>(formatters);
            }
            return responseObject;
        }

        public async Task<HttpResponseMessage> GetAsync(string path, string token, bool useBearerToken = true)
        {
            if (useBearerToken && !client.DefaultRequestHeaders.Contains("Authorization"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            return await client.GetAsync(path);
        }


        public async Task<Uri> PostAsync<T>(T entity, string path)
        {
            return await PostAsync<T>(entity, path, token: null, useBearerToken: false);
        }
        public async Task<Uri> PostAsync<T>(T entity, string path, string token, bool useBearerToken = true)
        {
            if (useBearerToken && !client.DefaultRequestHeaders.Contains("Authorization"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            HttpResponseMessage response = await client.PostAsJsonAsync(path, entity);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        public async Task<T> PutAsync<T>(T entity, string path)
        {
            return await PutAsync<T>(entity, path, token: null, useBearerToken: false);
        }
        public async Task<T> PutAsync<T>(T entity, string path, string token, bool useBearerToken = true)
        {
            if (useBearerToken && !client.DefaultRequestHeaders.Contains("Authorization"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            HttpResponseMessage response = await client.PutAsJsonAsync(path, entity); //(path + $"/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
            entity = await response.Content.ReadAsAsync<T>();
            return entity;
        }

        public async Task<HttpStatusCode> DeleteAsync(string id, string path)
        {
            return await DeleteAsync(id, path, token: null, useBearerToken: false);
        }
        public async Task<HttpStatusCode> DeleteAsync(string id, string path, string token, bool useBearerToken = true)
        {
            if (useBearerToken && !client.DefaultRequestHeaders.Contains("Authorization"))
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            HttpResponseMessage response = await client.DeleteAsync(string.Format("{0}//{1}",path,id));
            return response.StatusCode;
        }

        public async Task<JObject> Authenticate(string userName, string password, string clientId, string secret)
        {
            string data = string.Format("grant_type=password&username={0}&password={1}&client_id={2}&client_secret={3}", userName, password, clientId, secret);


            System.Diagnostics.Debug.WriteLine(client.BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //login data
            var formContent = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string, string>("grant_type", "password"),
                 new KeyValuePair<string, string>("username", userName),
                 new KeyValuePair<string, string>("password", password),
                 new KeyValuePair<string, string>("client_id", clientId),
                 new KeyValuePair<string, string>("client_secret", secret)
                 });

            //request
            HttpResponseMessage responseMessage = await client.PostAsync("/Token", formContent);

            //response body
            var responseJson = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                throw new TokenValidationException(JObject.Parse(json).GetValue("error_description").ToString());
            }
            //response body

            return JObject.Parse(responseJson);
        }
        public async Task<JObject> AuthenticateCustomGrantType(string grantType, string token, string clientId, string secret)
        {

            var formContent = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("grant_type", grantType),
                    new KeyValuePair<string, string>("code", token),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", secret)
                 });

            //request
            HttpResponseMessage responseMessage = await client.PostAsync("/Token", formContent);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                throw new TokenValidationException(JObject.Parse(json).GetValue("error_description").ToString());
            }
            //response body
            var responseJson = await responseMessage.Content.ReadAsStringAsync();
            return JObject.Parse(responseJson);
            //return JObject.Parse(responseJson).GetValue("access_token").ToString();
        }

    }

    public class TokenValidationException : ApplicationException
    {
        public TokenValidationException(string message) : base(message) { }
    }

}