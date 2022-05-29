﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.Client.ApiServices;
using Movies.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;
namespace Movies.Client.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieApiService movieApiService;

        public MoviesController(IMovieApiService movieApiService)
        {
            this.movieApiService = movieApiService;
        }

        // GET: Movies
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await LogTokenAndClaims();
            return View(await movieApiService.GetMovies());
            //return View(await _context.Movie.ToListAsync());
        }

        [Authorize(Roles ="admin")]
        public async Task<IActionResult> OnlyAdmin()
        {
            var userInfo =await movieApiService.GetUserInfo();
            return View(userInfo);
        }
        private async Task LogTokenAndClaims()
        {
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            Debug.WriteLine($"Identity token: {identityToken}");
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }
        // GET: Movies/Details/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Details(int? id)
        //{
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var movie = await _context.Movie
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (movie == null)
            //{
            //    return NotFound();
            //}

            //return View();
        //}

        // GET: Movies/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Genre,ReleaseDate,Owner,Rating,ImageUrl")] Movie movie)
        //{
        ////    if (ModelState.IsValid)
        ////    {
        ////        _context.Add(movie);
        ////        await _context.SaveChangesAsync();
        ////        return RedirectToAction(nameof(Index));
        ////    }
        //    return View();
        //}

        // GET: Movies/Edit/5
        //[HttpPut]
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    //if (id == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //var movie = await _context.Movie.FindAsync(id);
        //    //if (movie == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    //return View(movie);
        //    return View();
        //}

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,ReleaseDate,Owner,Rating,ImageUrl")] Movie movie)
        //{
        //    return View();
        //    //if (id != movie.Id)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //if (ModelState.IsValid)
        //    //{
        //    //    try
        //    //    {
        //    //        _context.Update(movie);
        //    //        await _context.SaveChangesAsync();
        //    //    }
        //    //    catch (DbUpdateConcurrencyException)
        //    //    {
        //    //        if (!MovieExists(movie.Id))
        //    //        {
        //    //            return NotFound();
        //    //        }
        //    //        else
        //    //        {
        //    //            throw;
        //    //        }
        //    //    }
        //    //    return RedirectToAction(nameof(Index));
        //    //}
        //    //return View(movie);
        //}

        // GET: Movies/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    return View();
        //    //if (id == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //var movie = await _context.Movie
        //    //    .FirstOrDefaultAsync(m => m.Id == id);
        //    //if (movie == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //return View(movie);
        //}

        // POST: Movies/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    //var movie = await _context.Movie.FindAsync(id);
        //    //_context.Movie.Remove(movie);
        //    //await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MovieExists(int id)
        //{
        //    //return _context.Movie.Any(e => e.Id == id);
        //    return true;
        //}
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
