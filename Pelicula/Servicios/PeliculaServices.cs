using Microsoft.EntityFrameworkCore;
using Pelicula.Models.Table;

namespace Pelicula.Servicios
{
    public class PeliculaServices
    {
        private readonly PeliculaContext context;
        //private IServiceProvider servicio;
        public PeliculaServices(PeliculaContext context)
        {
            this.context = context;
        }

        public async Task<List<PeliculaRepository>> GetAllPeliculaAsync()
        {
            return await  context.PeliculaRepositories.ToListAsync();
        }
        public async Task<PeliculaRepository?> GetPeliculaForId(int? id)
        {
            return await context.PeliculaRepositories.FirstOrDefaultAsync(m => m.IdPelicula == id);
 
        }
        public void CreatePelicula(PeliculaRepository p)
        {
            context.PeliculaRepositories.Add(p);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
        public async Task<PeliculaRepository?> FindPeliculaAsync(int? id)
        {
            var peliculaRepository = await context.PeliculaRepositories.FindAsync(id);
            return peliculaRepository;
        }

    }
}
