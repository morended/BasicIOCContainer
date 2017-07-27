using System.Linq;
using System.Web.Mvc;
using MoviesApp.Models;
using MoviesApp.Services;

namespace MoviesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService)
       {
           _movieService = movieService;
       }
        public ActionResult Index()
        {
            var model = _movieService.GetAllMovies();
            return View(model);
        }

        public ActionResult Detail(MovieDetailViewModel model)
        {
            Movie movie = _movieService.GetMovieDetail(model.Id - 1);
            return View(movie);
        }

        public ActionResult Add()
        {
            return View(new MovieViewModel());
        }

        [HttpPost]
        public ActionResult AddMovie(MovieViewModel model)
        {
            Movie movie = new Movie(_movieService.GetAllMovies().Count(), model.Title, "", model.ReleaseDate, model.Description);
            _movieService.AddMovie(movie);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}