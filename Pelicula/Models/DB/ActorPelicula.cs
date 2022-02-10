using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models.DB
{
    public partial class ActorPelicula
    {
        [Key]
        public int IdActoresPelicula { get; set; }
        public int IdPelicula { get; set; }
        public int IdActor { get; set; }

        public virtual Actor IdActorNavigation { get; set; } = null!;
        public virtual PeliculaRepository IdPeliculaNavigation { get; set; } = null!;
    }
}
