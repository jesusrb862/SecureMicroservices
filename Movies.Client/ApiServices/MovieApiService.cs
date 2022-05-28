using Movies.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json;
namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {

        IHttpClientFactory _httpClientFactory;
        public MovieApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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

            var request = new HttpRequestMessage(HttpMethod.Get,"/api/Movies/");

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

        public Task<Movie> UpdateMovie(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
