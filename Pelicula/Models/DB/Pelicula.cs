using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models.DB
{
    public partial class Pelicula
    {
        public Pelicula()
        {
            Generos = new HashSet<Genero>();
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
        public string? Actores { get; set; }
        public string? Director { get; set; }

        public virtual ICollection<Genero> Generos { get; set; }
        public virtual ICollection<PeliculaGenero> PeliculaGeneros { get; set; }
        public virtual ICollection<Puntuacion> Puntuacions { get; set; }
    }
}
