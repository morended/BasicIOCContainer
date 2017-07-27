using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesApp.Models
{
    public class Movie
    {
        public Movie(int id, string title, string imageUrl, string releaseDate, string description)
        {
            Id = id;
            Title = title;
            ImageUrl = imageUrl;
            ReleaseDate = releaseDate;
            Description = description;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ReleaseDate { get; set; }
        public string Description { get; set; }

    }
}