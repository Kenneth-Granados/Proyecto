using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models.DB
{
    public partial class DirectorPelicula
    {
        [Key]
        public int IdDirectorPelicula { get; set; }
        public int IdPelicula { get; set; }
        public int IdDirector { get; set; }

        public virtual Director IdDirectorNavigation { get; set; } = null!;
        public virtual PeliculaRepository IdPeliculaNavigation { get; set; } = null!;
    }
}
