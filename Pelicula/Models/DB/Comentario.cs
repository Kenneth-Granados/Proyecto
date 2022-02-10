using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models.DB
{
    public partial class Comentario
    {
        [Key]
        public int IdComentario { get; set; }
        public int? IdPelicula { get; set; }
        public string? IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario1 { get; set; } = null!;

        public virtual PeliculaRepository? IdPeliculaNavigation { get; set; }
        public virtual AspNetUser? IdUsuarioNavigation { get; set; }
    }
}
