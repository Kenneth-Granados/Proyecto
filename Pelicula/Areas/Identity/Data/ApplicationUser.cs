using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pelicula.Areas.Identity.Data;

// Agregue datos de perfil para usuarios de aplicaciones
// agregando propiedades a la clase ApplicationUser

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName ="bit")]
    public bool Estado { get; set; }
    
}

