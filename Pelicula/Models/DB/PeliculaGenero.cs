using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models.DB
{
    public partial class PeliculaGenero
    {
        [Key]
        public int IdPeliculaGenero { get; set; }
        public int? IdGenero { get; set; }
        public int? IdPelicula { get; set; }

        public virtual Genero? IdGeneroNavigation { get; set; }
        public virtual Pelicula? IdPeliculaNavigation { get; set; }
    }
}
