using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models.Table
{
    public partial class Actor
    {
        public Actor()
        {
            ActorPeliculas = new HashSet<ActorPelicula>();
        }
        [Key]
        public int IdActor { get; set; }
        public string FullName { get; set; } = null!;

        public virtual ICollection<ActorPelicula> ActorPeliculas { get; set; }
    }
}
