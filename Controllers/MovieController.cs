using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;
using System.Data.Entity;
namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _Context;
        public MovieController()
        {
            _Context = new ApplicationDbContext();
        }
        // GET: Movie
        public ActionResult Index()
        {
            var geners = _Context.Moves.Include(m => m.Genre);
            //CustomerViewModel customerViewModel = new CustomerViewModel
            //{
            //    Customers = GetCustomers()
            //};
            return View(geners);
        }
        [HttpPost]
        public ActionResult Save(Move movie)
        {
            if (movie.Id == 0)
                _Context.Moves.Add(movie);
            else
            {
                var movieInDb = _Context.Moves.SingleOrDefault(x => x.Id == movie.Id);
                //Mapper.Map(customer,customerInDb)
                movieInDb.Name = movie.Name;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
            }
            _Context.SaveChanges();
            return RedirectToAction("Index", "Movie");
        }
        public ActionResult Details(int id)
        {
            var movie = _Context.Moves.Include(c => c.Genre).FirstOrDefault(x => x.Id == id);
            var geners = _Context.Geners.ToList();
            if (movie != null)
            {
                var viewModel = new MovieFormViewModel
                {
                    Geners = geners,
                    Movie= movie
                };
                return View("MovieForm", viewModel);
            }
            return HttpNotFound();
        }
        public ActionResult New()
        {
            var geners = _Context.Geners.ToList();
            var viewModel = new MovieFormViewModel
            {
                Geners = geners
            };
            return View("MovieForm", viewModel);
        }
        public ActionResult Random()
        {
            var movie = new Move() { Name = "Rita" };
            //return View(movie);
            //return Content("Hello World");
            //return HttpNotFound();
            //return new EmptyResult();
            //ViewBag.RandomMovie = movie;
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
            List<Customer> customers = new List<Customer> {

                new Customer { Name="Cust1"},
                new Customer { Name="Cust2"},
            };
            RandomMovieViewModel viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }

    }
}