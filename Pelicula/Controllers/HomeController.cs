using Microsoft.AspNetCore.Mvc;
using Pelicula.Models;
using System.Diagnostics;
using Pelicula.Areas.Identity.Data;
using Pelicula.Data;
using Microsoft.EntityFrameworkCore;
using Pelicula.Models.DB;
using Microsoft.AspNetCore.Authorization;
using System.Linq;


namespace Pelicula.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PeliculaContext _context;


        public HomeController(ILogger<HomeController> logger,PeliculaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.PeliculaRepositories.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Index(string MovieSearch)
        {
            ViewData["GetMovies"] = MovieSearch;
            var moviequery = from x in _context.PeliculaRepositories select x;
            
            if (!String.IsNullOrEmpty(MovieSearch))
            {
                moviequery = moviequery.Where(x => x.Titulo.Contains(MovieSearch));
            }
            return View(await moviequery.AsNoTracking().ToListAsync());
        }
    
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var p = await _context.PeliculaRepositories.FindAsync(id);
            if(p == null)
            {
                return NotFound();
            }
            return View(p);
        }
        [HttpGet]
        public async Task<IActionResult> PeliculasGeneros(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var gen = await _context.Generos.FindAsync(id);
            if (gen == null)
            {
                return NotFound();
            }
            return View(await GetPeliculaPorGenero(id));
        }
        [HttpGet]
        public async Task<IActionResult> PeliculaActor(int id)
        {
            var consulta =
                from A in _context.Actors
                join AP in _context.ActorPeliculas on A.IdActor equals AP.IdActor
                join PR in _context.PeliculaRepositories on AP.IdPelicula equals PR.IdPelicula
                where A.IdActor == id
                select new PeliculaRepository
                {
                    IdPelicula = PR.IdPelicula,
                    Titulo = PR.Titulo,
                    Duracion = PR.Duracion,
                    Descripcion = PR.Descripcion,
                    LinkPelicula = PR.LinkPelicula,
                    LinkImagen = PR.LinkImagen
                };
            return View(consulta);
        }
        private async Task<IEnumerable<PeliculaRepository>> GetPeliculaPorGenero(int? id)
        {
            var consulta =

                    from G in _context.Generos
                    join PG in _context.PeliculaGeneros on G.IdGenero equals PG.IdGenero
                    join PR in _context.PeliculaRepositories on PG.IdPelicula equals PR.IdPelicula
                    where G.IdGenero == id
                    select new PeliculaRepository
                    {
                        IdPelicula = PR.IdPelicula,
                        Titulo = PR.Titulo,
                        Duracion = PR.Duracion,
                        Descripcion = PR.Descripcion,
                        LinkPelicula = PR.LinkPelicula,
                        LinkImagen = PR.LinkImagen
                    };

            return consulta;
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

        
    
}