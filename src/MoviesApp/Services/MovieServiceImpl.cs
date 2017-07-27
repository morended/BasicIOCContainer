using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesApp.data;
using MoviesApp.Models;

namespace MoviesApp.Services
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetAllMovies();
        Movie GetMovieDetail(int id);
        void AddMovie(Movie movie);

       
    }
    public class MovieServiceImpl : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieServiceImpl(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public IEnumerable<Movie> GetAllMovies()
        {
            return _movieRepository.GetMovies();
        }

        public Movie GetMovieDetail(int id)
        {
           return _movieRepository.GetMovieDetail(id);
        }

        public void AddMovie(Movie movie)
        {
             _movieRepository.AddMovie(movie);
        }
    }
}