using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pelicula.Areas.Identity.Data;
using Pelicula.Models.Table;

namespace Pelicula.Data;

public class PeliculaDBContext : IdentityDbContext<ApplicationUser>
//public class PeliculaDBContext : IdentityDbContext
{

    public PeliculaDBContext(DbContextOptions<PeliculaDBContext> options): base(options)
    {
    }

    //public  DbSet<Actor> Actors { get; set; } = null!;
    //public  DbSet<ActorPelicula> ActorPeliculas { get; set; } = null!;
    //public  DbSet<Comentario> Comentarios { get; set; } = null!;
    //public  DbSet<Director> Directors { get; set; } = null!;
    //public  DbSet<DirectorPelicula> DirectorPeliculas { get; set; } = null!;
    //public  DbSet<Genero> Generos { get; set; } = null!;
    //public  DbSet<PeliculaGenero> PeliculaGeneros { get; set; } = null!;
    //public  DbSet<PeliculaRepository> PeliculaRepositories { get; set; } = null!;
    //public  DbSet<Puntuacion> Puntuacions { get; set; } = null!;
}
