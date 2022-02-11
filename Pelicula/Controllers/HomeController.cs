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
            PeliculaDetail d = new PeliculaDetail();
            d.PeliculaBase = p;

            // Se cargan la lista de actores de la pelicula
            await Task.Run( async () =>
            {
                var l =
                        from A in _context.Actors
                        join AP in _context.ActorPeliculas on A.IdActor equals AP.IdActor
                        where AP.IdPelicula == d.PeliculaBase.IdPelicula
                        select new Actor
                        {
                            IdActor = A.IdActor,
                            FullName = A.FullName
                        };
                d.ListActores = await l.ToListAsync();
            });
            //Se cargan la lista de generos de la pelicual
            await Task.Run(async () =>
            {
                var g =
                    from PR in _context.PeliculaRepositories
                    join PG in _context.PeliculaGeneros on PR.IdPelicula equals PG.IdPelicula
                    join G in _context.Generos on PG.IdGenero equals G.IdGenero
                    where PR.IdPelicula == d.PeliculaBase.IdPelicula
                    select new Genero
                    {
                        IdGenero = G.IdGenero,
                        NombreGenero = G.NombreGenero
                    };
                d.ListGenero = await g.ToListAsync();
            });
            return View(d);
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
            ViewBag.Message = gen.NombreGenero;
            return View(await GetPeliculaPorGenero(id));
        }
        [HttpGet]
        public async Task<IActionResult> PeliculaActor(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }
            ViewBag.Message = actor.FullName;
            List<PeliculaRepository> consulta = new List<PeliculaRepository>();
            await Task.Run(async () =>
            {
                var c =
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
                consulta = await c.ToListAsync();

           });
            return View(consulta);
        }
        private async Task<IEnumerable<PeliculaRepository>> GetPeliculaPorGenero(int? id)
        {
            List<PeliculaRepository> consulta = new List<PeliculaRepository>();
            await Task.Run(async () =>
            {
                var c =
                    from PG in _context.PeliculaGeneros 
                    join PR in _context.PeliculaRepositories on PG.IdPelicula equals PR.IdPelicula
                    where PG.IdGenero == id
                    select new PeliculaRepository
                    {
                        IdPelicula = PR.IdPelicula,
                        Titulo = PR.Titulo,
                        Duracion = PR.Duracion,
                        Descripcion = PR.Descripcion,
                        LinkPelicula = PR.LinkPelicula,
                        LinkImagen = PR.LinkImagen
                    };

                consulta = await c.ToListAsync();
            });
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