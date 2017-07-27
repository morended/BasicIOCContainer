using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using MoviesApp.Models;

namespace MoviesApp.data
{
    public interface IMovieRepository
    {

       IList<Movie> GetMovies();
       void AddMovie(Movie movie);

        Movie GetMovieDetail(int id);

    }

    public class MovieRepositoryImpl : IMovieRepository
    {
        private IList<Movie> _moviesList;

        public MovieRepositoryImpl()
        {
            _moviesList = new List<Movie>()
            {
                new Movie(1, "Doctor Strange", "1", DateTime.Today.ToLongDateString(),
                    "Description : Doctor strange"),
                new Movie(2, "Moana", "2", DateTime.Today.ToLongDateString(), "Description : Moana"),
                new Movie(3, "Trolls", "3", DateTime.Today.ToLongDateString(), "Description :Trolls"),
                new Movie(4, "Arrival", "4", DateTime.Today.ToLongDateString(), "Description : Arrival"),
                new Movie(5, "Almost Christmas", "5", DateTime.Today.ToLongDateString(),
                    "Description : Almost Christmas")
            };
        }

        public IList<Movie> GetMovies()
        {
            return _moviesList;
        }

        public void AddMovie(Movie movie)
        {
            _moviesList.Add(movie);
        }

        public Movie GetMovieDetail(int id)
        {
            return _moviesList[id];
        }
    }
}