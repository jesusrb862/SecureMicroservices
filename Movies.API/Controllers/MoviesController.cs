using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.Model;

namespace Movies.API.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    [Authorize("ClientPolicy")]
   // [Authorize ("ClientPolicy")]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesAPIContext _context;

        public MoviesController(MoviesAPIContext context)
        {
            _context = context;
        }

        // GET: Movies
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            return await _context.Movie.ToListAsync();
        }

        // GET: Movies/Details/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Movie>> GetMovie(int id)
        //{
        //    var movie = await _context.Movie
        //        .FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return movie;
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Movie>> PutMovie(int id, Movie movie)
        //{
        //    if (id != movie.Id)
        //    {
        //        return BadRequest();
        //    }
        //    _context.Entry(movie).State = EntityState.Modified;
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MovieExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return NoContent();
        //}

        //private bool MovieExists(int id) => _context.Movie.Any(p => p.Id == id);

        //[HttpPost]
        //public async Task<ActionResult<Movie>> PostMovie(Movie movie){

        //    _context.Movie.Add(movie);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction("GetMovie", new {id = movie.Id },movie);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteMovie(int id)
        //{
        //    var movie= await _context.Movie.FindAsync(id);
        //    if(movie == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Movie.Remove(movie);
        //    await _context.SaveChangesAsync();

        //    return NoContent(); 
        //}
    }
}
