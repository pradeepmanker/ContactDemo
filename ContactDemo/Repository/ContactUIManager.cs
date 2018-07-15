using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ContactDemo.Models;
using ContactDemo.Models.Validations;

namespace ContactDemo.Repository
{
    public class ContactUIManager : IContactUIManager
    {
        private string url = System.Configuration.ConfigurationManager.AppSettings["APIServiceURL"];
        private HttpClient _client;

        public ContactUIManager()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpClient Client
        {
            get
            {
                return _client;
            }
        }

        public IEnumerable<ContactMaster> GetAllContacts()
        {
            HttpResponseMessage responseMessage = Client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var contacts = JsonConvert.DeserializeObject<List<ContactMaster>>(responseData);

                return contacts;
            }
            return null;
        }

        public ContactMaster GetContact(Int64 id)
        {
            HttpResponseMessage responseMessage = Client.GetAsync(url + "/" + id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var contacts = JsonConvert.DeserializeObject<ContactMaster>(responseData);

                return contacts;
            }
            return null;
        }

        public ContactMaster CreateContact()
        {
            return new ContactMaster();
        }

        public ApiResponse CreateContact(ContactMaster contact)
        {
            HttpResponseMessage responseMessage = Client.PostAsJsonAsync(url, contact).Result;
            var responseData = CreateJsonResponse<ApiResponse>(responseMessage).Result;
            return responseData;
        }

        public ContactMaster EditContact(Int64 id)
        {
            HttpResponseMessage responseMessage = Client.GetAsync(url + "/" + id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var contact = JsonConvert.DeserializeObject<ContactMaster>(responseData);

                return contact;
            }
            return null;
        }

        public ApiResponse EditContact(Int64 id, ContactMaster contact)
        {
            HttpResponseMessage responseMessage = Client.PutAsJsonAsync(url + "/" + id, contact).Result;
            var responseData = CreateJsonResponse<ApiResponse>(responseMessage).Result;
            return responseData;
        }

        public ContactMaster Delete(Int64 id)
        {
            HttpResponseMessage responseMessage = Client.GetAsync(url + "/" + id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var contact = JsonConvert.DeserializeObject<ContactMaster>(responseData);

                return contact;
            }
            return null;
        }

        public bool Delete(Int64 id, ContactMaster contact)
        {
            HttpResponseMessage responseMessage = Client.DeleteAsync(url + "/" + id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


        private static async Task<TResponse> CreateJsonResponse<TResponse>
    (HttpResponseMessage response) where TResponse : ApiResponse, new()
        {
            var clientResponse = new TResponse
            {
                StatusIsSuccessful = response.IsSuccessStatusCode,
                ErrorState = response.IsSuccessStatusCode ? null :
                    await DecodeContent<ErrorStateResponse>(response),
                ResponseCode = response.StatusCode
            };
            if (response.Content != null)
            {
                clientResponse.ResponseResult = await response.Content.ReadAsStringAsync();
            }

            return clientResponse;
        }

        private static async Task<TContentResponse>
            DecodeContent<TContentResponse>(HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TContentResponse>(result);
        }
        
    }
}