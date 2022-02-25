using Microsoft.AspNetCore.Mvc;
using Pelicula.Models;
using System.Diagnostics;
using Pelicula.Areas.Identity.Data;
using Pelicula.Data;
using Microsoft.EntityFrameworkCore;
using Pelicula.Models.Table;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Pelicula.Servicios;

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
            var moviequery = from x in _context.PeliculaRepositories  select x;
            
            if (!String.IsNullOrEmpty(MovieSearch))
            {
                moviequery = moviequery
                    .Where(x => x.Titulo.Contains(MovieSearch))
                    .OrderByDescending(d => d.IdPelicula);
            }
            return View(await moviequery.AsNoTracking().ToListAsync());
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddComentario(string Comentario, int IdPelicula, string UserName)
        {
            var id = await _context.PeliculaRepositories.FindAsync(IdPelicula);
            var us = await _context.AspNetUsers.FindAsync(UserName);

            if (id == null || us == null)
            {
                return Json(new { isValid = true, html = "" });
            }
            else
            {
                _context.Comentarios.Add(new Comentario
                {
                    IdPelicula = id.IdPelicula,
                    IdUsuario = us.Id,
                    Fecha = DateTime.Now,
                    Comentario1 = Comentario
                });
                await _context.SaveChangesAsync();
                List<ComentarioDetail> ListComentarios = new List<ComentarioDetail>();
                await Task.Run(async () => {
                    var c =
                        from CM in _context.Comentarios
                        join U in _context.AspNetUsers on CM.IdUsuario equals U.Id
                        orderby CM.IdComentario descending
                        where CM.IdPelicula == id.IdPelicula
                        select new ComentarioDetail
                        {
                            UserName = U.UserName,
                            Fecha = DateTime.Now,
                            Comentario = CM.Comentario1
                        };
                    ListComentarios = await c.ToListAsync();
                });
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ComentarioPartial", ListComentarios) });
            }
                //return Json(new { isValid = true, cagada = "Cadena con texto = "+ Comentario + " = "+ UserName });
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
            // Se cargan la lista de los comentarios de la pelicula
            await Task.Run( async ()=>{
                var c =
                    from CM in _context.Comentarios 
                    join U in _context.AspNetUsers on CM.IdUsuario equals U.Id
                    where CM.IdPelicula == d.PeliculaBase.IdPelicula
                    select new ComentarioDetail
                    {
                        UserName = U.UserName,
                        Fecha = DateTime.Now,
                        Comentario = CM.Comentario1
                    };
                d.ListComentarios = await c.ToListAsync();
            });
            // 29590edb-a830-4926-b539-b1a2dad2ebbe
            // 29590edb-a830-4926-b539-b1a2dad2ebbe
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
                orderby PR.IdPelicula descending
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
                    orderby PR.IdPelicula descending
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

        public JsonResult AddComment(ComentarioViewModel comentario)
        {

            return Json(1);
        }
    }

        
    
}