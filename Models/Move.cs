using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Move
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Please enter customer's name.")]
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? DateAdded { get; set; }
        public byte NumberInStock { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
    }
}