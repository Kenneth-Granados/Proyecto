using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models.DB
{
    public partial class Genero
    {
        public Genero()
        {
            PeliculaGeneros = new HashSet<PeliculaGenero>();
        }
        [Key]
        public int IdGenero { get; set; }
        public string? NombreGenero { get; set; }

        public virtual ICollection<PeliculaGenero> PeliculaGeneros { get; set; }
    }
}
