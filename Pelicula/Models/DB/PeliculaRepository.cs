using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models.DB
{
    public partial class PeliculaRepository
    {
        public PeliculaRepository()
        {
            ActorPeliculas = new HashSet<ActorPelicula>();
            Comentarios = new HashSet<Comentario>();
            DirectorPeliculas = new HashSet<DirectorPelicula>();
            PeliculaGeneros = new HashSet<PeliculaGenero>();
            Puntuacions = new HashSet<Puntuacion>();
        }
        [Key]
        public int IdPelicula { get; set; }
        public string Titulo { get; set; } = null!;
        public TimeSpan Duracion { get; set; }
        public string? Descripcion { get; set; }
        public string? LinkPelicula { get; set; }
        public string? LinkImagen { get; set; }
   
    
        public virtual ICollection<ActorPelicula> ActorPeliculas { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<DirectorPelicula> DirectorPeliculas { get; set; }
        public virtual ICollection<PeliculaGenero> PeliculaGeneros { get; set; }
        public virtual ICollection<Puntuacion> Puntuacions { get; set; }
    }
}
