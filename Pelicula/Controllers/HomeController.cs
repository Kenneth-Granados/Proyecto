using Microsoft.AspNetCore.Mvc;
using Pelicula.Models;
using System.Diagnostics;
using Pelicula.Areas.Identity.Data;
using Pelicula.Data;
using Microsoft.EntityFrameworkCore;

namespace Pelicula.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private PeliculaDBContext pdb;

        public HomeController(ILogger<HomeController> logger, PeliculaDBContext pdb)
        {
            _logger = logger;
            this.pdb=pdb;
        }

        public async Task<IActionResult> Index()
        {
            var peli = await pdb.PeliDB.ToListAsync();
            return View(peli);
        }

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