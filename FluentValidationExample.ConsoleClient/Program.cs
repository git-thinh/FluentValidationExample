using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentValidationExample.Models;
using Nito.AsyncEx.Synchronous;

namespace FluentValidationExample.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            var personModel = p.Get();
            PersonCreateRequestModel goodModel = new PersonCreateRequestModel() {Firstname = "Alfred", Lastname = "Andrews"} ;
            PersonCreateRequestModel badModel = new PersonCreateRequestModel() { Firstname = "", Lastname = "Andrews" };

            var task = p.PostToService(goodModel);
            WebApiResponse<Guid> goodResult = task.WaitAndUnwrapException();
            p.ProcessResult(goodResult);

            var task2 = p.PostToService(badModel);
            WebApiResponse<Guid> badResult = task2.WaitAndUnwrapException();
            p.ProcessResult(badResult);

        }

        private async Task<WebApiResponse<Guid>> PostToService(PersonCreateRequestModel model)
        {
            var httpClient = GetHttpClient();
            string requestEndpoint = "person"; // full request will be http://localhost:5802/api/person
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(requestEndpoint, model);

            WebApiResponse<Guid> wrappedResponse;
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var id = await response.Content.ReadAsAsync<Guid>();
                wrappedResponse = new WebApiResponse<Guid>(id, response.StatusCode);
            }
            else
            {
                var errors = await response.Content.ReadAsStringAsync();
                wrappedResponse = new WebApiResponse<Guid>(errors, response.StatusCode, true);
            }
            return wrappedResponse;
        }

        private async Task<PersonModel> Get()
        {
            var httpClient = GetHttpClient();
            string requestEndpoint = "person"; // full request will be http://localhost:5802/api/person
            HttpResponseMessage response = await httpClient.GetAsync(requestEndpoint);
            PersonModel person = null;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                person = await response.Content.ReadAsAsync<PersonModel>();
            }

            return person;
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@"http://localhost:5802/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        private void ProcessResult(WebApiResponse<Guid> result)
        {
            if (result.HttpStatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine($"{result.ApiResponse}");
            }
            else
            {
                Console.WriteLine($"{result.Error}");
            }
        }
    }
}
