using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models.Table
{
    public partial class Visualizacione
    {
        public int IdVisualizaciones { get; set; }
        public int IdPelicula { get; set; }
        public DateTime Fecha { get; set; }

        public virtual PeliculaRepository IdPeliculaNavigation { get; set; } = null!;
    }
}
