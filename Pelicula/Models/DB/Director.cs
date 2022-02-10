using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models.DB
{
    public partial class Director
    {
        public Director()
        {
            DirectorPeliculas = new HashSet<DirectorPelicula>();
        }
        [Key]
        public int IdDirector { get; set; }
        public string FullNombre { get; set; } = null!;

        public virtual ICollection<DirectorPelicula> DirectorPeliculas { get; set; }
    }
}
