﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models.DB
{
    public partial class Comentario
    {
        [Key]
        public int IdComentario { get; set; }
        public string? IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario1 { get; set; } = null!;

        public virtual AspNetUser? IdUsuarioNavigation { get; set; }
    }
}
