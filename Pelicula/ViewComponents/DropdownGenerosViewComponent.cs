using Microsoft.AspNetCore.Mvc;
using Pelicula.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace Pelicula.ViewComponents
{
    public class DropdownGenerosViewComponent : ViewComponent
    {
        private readonly PeliculaContext _context;
        public DropdownGenerosViewComponent(PeliculaContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cm = await _context.Generos.ToListAsync();
            return View(cm);
        }
    }
}
