using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models.Table
{
    public partial class Puntuacion
    {
        [Key]
        public int IdPuntuacion { get; set; }
        public string? IdUsuario { get; set; }
        public int? IdPelicula { get; set; }

        public virtual PeliculaRepository? IdPeliculaNavigation { get; set; }
        public virtual AspNetUser? IdUsuarioNavigation { get; set; }
    }
}
