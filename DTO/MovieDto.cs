
using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.DTO
{
    public class MovieDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        
        public byte GenreId { get; set; }

        public DateTime DateAdded { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public byte NumberInStock { get; set; }
    }
}