using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Pelicula.Models.DB
{
    public partial class Puntuacion
    {
        [Key]
        public int IdPuntuacion { get; set; }
        public string IdUsuario { get; set; } = null!;
        public int? IdPelicula { get; set; }

        public virtual Pelicula? IdPeliculaNavigation { get; set; }
    }
}
