
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace Pelicula.Models
{
    public class DisplayRol
    {
        [Required(ErrorMessage = "Campo obligatorio",AllowEmptyStrings = false)]
        public string role { get; set; }
        [ValidateNever]
        public List<IdentityRole> roles { get; set; }
    }
}
