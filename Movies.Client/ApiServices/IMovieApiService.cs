
using Movies.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Client.ApiServices
{
    public interface IMovieApiService
    {
      Task<IEnumerable<Movie>> GetMovies();
      Task<Movie> GetMovie(string id);
      Task<Movie> UpdateMovie(string id);
      Task DeleteMovie(string id);

      Task<UserInfoViewModel> GetUserInfo();
    }
}
