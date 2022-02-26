using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pelicula.Areas.Identity.Data;
using Pelicula.Models;
using Pelicula.Models.Table;

namespace Pelicula.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleManagementController : Controller
    {
        // Administrador de usuarios
        private readonly UserManager<ApplicationUser> userManager;
        // Administrador de funciones
        private readonly RoleManager<IdentityRole> roleManager;
        // Clase de contexto de datos
        private readonly PeliculaContext _context;

        public RoleManagementController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, PeliculaContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            //obtener todos los usuarios y los envia a la vista
            var name = userManager.GetUserName(User);
            List<ApplicationUser> users = new List<ApplicationUser>();
            if(name == null)
            {
                users = userManager.Users.ToList();
            }
            else
            {
                users = userManager.Users.Where(d => d.UserName != name).ToList();   //ToList().Where(d => d.UserName != name).ToList();
            }
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string userId)
        {
            if(userId == null)
            {
                return RedirectToAction(nameof(PageNotFound));
            }
            //Encontrar usuario por ID de usuario
            // Agregar nombre de usuario a ViewBag
            //obtener userRole de usuarios y enviar para ver
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.Error = "El Id del Usuario no es valido";
                return RedirectToAction(nameof(PageNotFound));
            }
            ViewBag.UserName = user.UserName;
            var userRoles = await userManager.GetRolesAsync(user);
            return View(userRoles);
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return RedirectToAction(nameof(DisplayRoles));
        }
        //[HttpPost]
        //public async Task<IActionResult> AddRole(string role)
        //{
        //    //crear un nuevo rol usando roleManager
        //    //return to displayRoles
        //    await roleManager.CreateAsync(new IdentityRole(role));
        //    return RedirectToAction(nameof(DisplayRoles));
        //}
        [HttpGet]
        public IActionResult DisplayRoles()
        {
            //Obtiene todos los roles y se los pasa a la vista
            DisplayRol r = new DisplayRol();
            r.roles = roleManager.Roles.ToList();
            return View(r);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisplayRoles([Bind("role,roles")] DisplayRol rl)
        {
            if (!ModelState.IsValid)
            {
                rl.roles = roleManager.Roles.ToList();
                return View(rl);
            }
            await roleManager.CreateAsync(new IdentityRole(rl.role));
            return RedirectToAction(nameof(DisplayRoles));
        }
        [HttpGet]
        public IActionResult AddUserToRole()
        {
            //Optiene todos los usuarios
            //Optiene todos los roles
            //crear lista de selección y pasar usando viewBag
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();

            // Primer paramentro es el que se optiene y el segundo es el que se muetra
            ViewBag.Users = new SelectList(users, "Id", "UserName");
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddUserToRole([Bind("UserId,RoleName")] UserRole userRole)
        {
            // Valida que el modelo sea valido
            if (ModelState.IsValid)
            {
                //Encuentra el usuario por el ID
                //Asigna el rol al usuario
                //Redirecciona al index
                var user = await userManager.FindByIdAsync(userRole.UserId);
                await userManager.AddToRoleAsync(user, userRole.RoleName);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var users = userManager.Users.ToList();
                var roles = roleManager.Roles.ToList();

                ViewBag.Users = new SelectList(users, "Id", "UserName");
                ViewBag.Roles = new SelectList(roles, "Name", "Name");
                return View(userRole);
            }

        }
        [HttpGet]
        public async Task<IActionResult> RemoveUserRole(string role, string userName)
        {
            //get user from userName
            //remove role of user using userManager
            //return to details with parameter userId

            var user = await userManager.FindByNameAsync(userName);

            var result = await userManager.RemoveFromRoleAsync(user, role);

            return RedirectToAction(nameof(Details), new { userId = user.Id });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveRole(string role)
        {
            //Se optiene el nombre del rol mediante su nombre
            //Elimina el rol usando roleManager
            //Redirecciona a Displayroles

            var roleToDelete = await roleManager.FindByNameAsync(role);
            var result = await roleManager.DeleteAsync(roleToDelete);

            return RedirectToAction(nameof(DisplayRoles));
        }
        // GET: PeliculaRepositories/Delete/5
        public async Task<IActionResult> Delete(string? userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var user = await _context.AspNetUsers.FindAsync (userId);
            if (user == null)
            {
                return NotFound();
            }
            
            return View(user);

        }

        //POST: PeliculaRepositories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string userId)
        {
            //var userRemove = await _context.AspNetUsers.FindAsync(userId);
            var user = await userManager.FindByIdAsync(userId);
            await userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        [Route("404")]
        public IActionResult PageNotFound()
        {
            return View();
        }


    }
}
