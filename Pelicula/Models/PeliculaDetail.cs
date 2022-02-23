using Pelicula.Models.DB;

namespace Pelicula.Models
{
    public class PeliculaDetail
    {
      
        public PeliculaRepository PeliculaBase { get; set; }
        // Atributos Extras
        public List<Actor> ListActores { get; set; }
        public List<ComentarioDetail> ListComentarios { get; set; }
        public List<Genero> ListGenero { get; set; }
    }
}
