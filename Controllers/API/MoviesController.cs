using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTO;
using Vidly.Models;


namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        //Get/api/customer
        private ApplicationDbContext _Context;
        public MoviesController()
        {
            _Context = new ApplicationDbContext();
        }
        public IEnumerable<MovieDto> GetCustomerMovies()
        {
            return _Context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
        }
        public MovieDto GetMovie(int CustomerId)
        {
            var movie = _Context.Movies.FirstOrDefault(x => x.Id == CustomerId);
            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Movie, MovieDto>(movie);
        }
        [HttpPost]
        public MovieDto CreateMovie(MovieDto movie)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var mov = Mapper.Map<MovieDto, Movie>(movie);
            _Context.Movies.Add(mov);
            _Context.SaveChanges();
            movie.Id = mov.Id;
            return movie;
        }
        [HttpPut]
        public void UpdateMovie(MovieDto movie)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var oldCustomer = _Context.Movies.FirstOrDefault(x => x.Id == movie.Id);
            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map(movie, oldCustomer);
            //oldCustomer.DateAdded = movie.DateAdded;
            //oldCustomer.Name = movie.Name;
            //oldCustomer.GenreId = movie.GenreId;
            //oldCustomer.NumberInStock = movie.NumberInStock;
            _Context.SaveChanges();
        }
        [HttpDelete]
        public void DeleteMovie(int movieId)
        {
            var movie = _Context.Movies.FirstOrDefault(x => x.Id == movieId);
            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _Context.Movies.Remove(movie);
            _Context.SaveChanges();
        }
    }
}
