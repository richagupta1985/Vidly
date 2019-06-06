using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Geners { get; set; }
        public Move Movie { get; set; }

        public string Title
        {
            get
            {
                return Movie.Id != 0 ? "Edit Movie" : "New Movie";
            }
        }
    }
}