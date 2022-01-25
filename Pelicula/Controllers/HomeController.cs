using Microsoft.AspNetCore.Mvc;
using Pelicula.Models;
using System.Diagnostics;
using Pelicula.Areas.Identity.Data;
using Pelicula.Data;
using Microsoft.EntityFrameworkCore;
using Pelicula.Models.DB;
using Microsoft.AspNetCore.Authorization;

namespace Pelicula.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  PeliculaDBContext pdb;
   

        public HomeController(ILogger<HomeController> logger, PeliculaDBContext pdb)
        {
            _logger = logger;
            this.pdb = pdb;
        }

        public async Task<IActionResult> Index()
        {
            List<PeliculaRepository> p = null;
            using (var db = new PeliculaContext())
            {
                p = await db.PeliculaRepositories.ToListAsync(); 
            }

            return View(p);
        }
        //[Authorize]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PeliculaRepository p = null;
            using(var db = new PeliculaContext())
            {
                p = await db.PeliculaRepositories.FindAsync(id);
            }
            return View(p);
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