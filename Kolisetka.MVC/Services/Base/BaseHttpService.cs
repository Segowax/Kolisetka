using Kolisetka.MVC.Contracts;
using System.Net.Http.Headers;

namespace Kolisetka.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected readonly ILocalStorageService _localStorageService;
        protected IClient _client;

        public BaseHttpService(IClient client, ILocalStorageService localStorageService)
        {
            _client = client;
            _localStorageService = localStorageService;
        }

        protected Response ConvertApiExceptions(ApiException ex)
        {
            if (ex.StatusCode == 422)
                return new Response() { Message = "Validation erros have occured.", ValidationError = ex.Response, Success = false };
            else if (ex.StatusCode == 404)
                return new Response() { Message = "The requested item could not be found.", Success = false };
            else
                return new Response() { Message = "Something went wrong, please try again.", Success = false };
        }

        protected void AddBearerToken()
        {
            if (_localStorageService.Exists("token"))
                _client.HttpClient.DefaultRequestHeaders.Authorization 
                    = new AuthenticationHeaderValue("Bearer", _localStorageService.GetStorageValue<string>("token"));
        }
    }
}
