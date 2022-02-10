using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Pelicula.Models
{
    public class PeliculaFull
    {
        [Key]
        public int IdPelicula { get; set; }
        [Required]
        public string Titulo { get; set; } = null!;
        [Required]
        public TimeSpan Duracion { get; set; }
        [Required]
        public string? Descripcion { get; set; }
        [Url]
        [Required]
        public string? LinkPelicula { get; set; }
        [Url]
        [Required]
        public string? LinkImagen { get; set; }



        [Remote("ValidaNombreRemoto", "PeliculaRepositories",ErrorMessage = "Formato no valido")]
        [Display(Name = "Ingresa El nombre del actores separados por ( , )")]
        [Required(ErrorMessage = "Tienes que completar este campo",AllowEmptyStrings = false)]
        public string ListaActores { get; set; }

        [Display(Name = "Generos Disponibles")]
        [Required(ErrorMessage = "Selecciona almenos un genero")]
        public int[] GenerosIds { get; set; }
        
        



        public List<SelectListItem> ListGeneros { get; set; }
    }
}
