using Movies.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MovieApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task DeleteMovie(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Movie> GetMovie(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/movies");

            var response = await httpClient
                .SendAsync(request,HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

           var movieList=  JsonConvert.DeserializeObject<List<Movie>>(content);

            return movieList;

           // var apiCredentials = new ClientCredentialsTokenRequest
           // {
           //     Address = "https://localhost:5005/connect/token",
           //     ClientId = "movieClient",
           //     ClientSecret = "secret",
           //     Scope = "movieAPI"
           // };

            // HttpClient client = new HttpClient();

            //var disco=  await client.GetDiscoveryDocumentAsync("https://localhost:5005");

            //if( disco.IsError)
            //{
            //     return null;
            //}

            //var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiCredentials);

            // if (tokenResponse.IsError)
            // {
            //     return null;
            // }

            // HttpClient apiClient = new HttpClient();
            // apiClient.SetBearerToken(tokenResponse.AccessToken);

            // var response = await apiClient.GetAsync("https://localhost:5001/api/Movies");

            // response.EnsureSuccessStatusCode();

            // var content = await response.Content.ReadAsStringAsync();

            // List<Movie> movieList =  JsonConvert.DeserializeObject<List<Movie>>(content);

            // return movieList;


        }

        public async Task<UserInfoViewModel> GetUserInfo()
        {
            var idpClient = _httpClientFactory.CreateClient("IDPClient");
            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();
            if (metaDataResponse.IsError)
            {
                throw new HttpRequestException("Somethig went wrong");
            }

            var accesstoken =await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var userInfoResponse = await idpClient.GetUserInfoAsync(
                   new UserInfoRequest
                   {
                       Address = metaDataResponse.UserInfoEndpoint,
                       Token = accesstoken
                   });

            if (userInfoResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong");
            }

            var userInfoDictionary = new Dictionary<string, string>();

            foreach (var claim in userInfoResponse.Claims)
            {
                userInfoDictionary.Add(claim.Type,claim.Value);
            }

            return new UserInfoViewModel(userInfoDictionary);
        }
        

        public Task<Movie> UpdateMovie(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
