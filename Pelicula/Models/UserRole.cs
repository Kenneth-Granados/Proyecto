
using System.ComponentModel.DataAnnotations;
namespace Pelicula.Models
{
    public class UserRole
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string RoleName { get; set; }
    }
}
