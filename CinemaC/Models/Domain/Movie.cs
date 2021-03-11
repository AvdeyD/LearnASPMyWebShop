using System.ComponentModel.DataAnnotations;
using System.Web.Razor;

namespace CinemaC.Models.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public Genre[] Genres { get; set; }
        public int MinAge { get; set; }
        public string Director { get; set; }
        public string ImgUrl { get; set; }
        public double Rating { get; set; }
        public int? ReleaseDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}