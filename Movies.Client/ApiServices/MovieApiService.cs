using Movies.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
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
            var movieList = new List<Movie>()
            {
                new()
                {
                     Id= 1,
                     Genre="Comics",
                     Title="asd",
                     Rating="9.2",
                     ImageUrl="images/src",
                     ReleaseDate=System.DateTime.Now,
                     Owner="swn"
                }
            };
            return await Task.FromResult(movieList);
        }

        public Task<Movie> UpdateMovie(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
