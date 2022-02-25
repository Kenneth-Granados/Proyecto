#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pelicula.Models.Table;
using Pelicula.Models;
using System.Collections;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Syncfusion.EJ2.Spreadsheet;

namespace Pelicula.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PeliculaRepositoriesController : Controller
    {
        private readonly PeliculaContext _context;

        public PeliculaRepositoriesController(PeliculaContext context)
        {
            _context = context;
        }

        // GET: PeliculaRepositories
        public async Task<IActionResult> Index()
        {
            return View(await _context.PeliculaRepositories.Select(m => new PeliculaLite { IdPelicula = m.IdPelicula, Titulo = m.Titulo }).ToListAsync());
        }

        // GET: PeliculaRepositories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peliculaRepository = await _context.PeliculaRepositories
                .FirstOrDefaultAsync(m => m.IdPelicula == id);
            if (peliculaRepository == null)
            {
                return NotFound();
            }

            return View(peliculaRepository);
        }

        // GET: PeliculaRepositories/Create
        public async Task<IActionResult> Create()
        {
            PeliculaFull f = new PeliculaFull();
            f.ListGeneros = await GetAllGeneros();
            ViewBag.Prueba = "";
            return View(f);
        }
        private async Task<List<SelectListItem>> GetAllGeneros()
        {
            var generos = await _context.Generos.ToListAsync();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in generos)
            {
                list.Add(new SelectListItem { Text = item.NombreGenero, Value = item.IdGenero.ToString() });
            }
            return list;
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PeliculaFull full)
        {
            //    return RedirectToAction(nameof(Index));
            if (ModelState.IsValid)
            {
                /*
                using(var trasn = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var parametroId = new SqlParameter("@IdPelicula", SqlDbType.Int);
                        parametroId.Direction = ParameterDirection.Output;

                        await _context.Database.ExecuteSqlInterpolatedAsync
                        (
                            $@"EXEC SP_InsertMovie 
                            @IdPelicula={parametroId} OUTPUT, 
                            @Titulo={full.Titulo},
                            @Duracion = {full.Duracion},
                            @Descripcion = {full.Descripcion},
                            @LinkPelicula = {full.LinkPelicula},
                            @LinkImagen = {full.LinkImagen}"
                        );
                        var idPeliculaInsertada = (int)parametroId.Value;
                        List<int> listIdActores = new List<int>();
                    
                        foreach (var item in SplitFullList(full.ListaActores))
                        {
                            var SearchActor = await _context.Actors.FirstOrDefaultAsync(m => m.FullName == item);
                            if(SearchActor != null)
                            {
                                listIdActores.Add(SearchActor.IdActor);
                            }
                            else
                            {
                                var parametroIdActor = new SqlParameter("@IdActor", SqlDbType.Int);
                                parametroIdActor.Direction = ParameterDirection.Output;
                                await _context.Database.ExecuteSqlInterpolatedAsync($@"EXEC SP_InsertActor @IdActor={parametroIdActor} OUTPUT, @FullName ={item}");
                                var IdActorInsertado = (int)parametroIdActor.Value;
                                listIdActores.Add(IdActorInsertado);
                            }
                        }
                
                        List<ActorPelicula> relacionPeliculaActor = new List<ActorPelicula>();
                        foreach (var item in listIdActores)
                        {
                            relacionPeliculaActor.Add
                                (
                                    new ActorPelicula 
                                    { 
                                        IdPelicula = idPeliculaInsertada, 
                                        IdActor = item 
                                    }
                                );
                        }
                        await _context.ActorPeliculas.AddRangeAsync(relacionPeliculaActor);
                    
                        List<PeliculaGenero> relacionPeliculaGenero = new List<PeliculaGenero>();
                        foreach (var item in full.GenerosIds)
                        {
                            relacionPeliculaGenero.Add
                                (
                                    new PeliculaGenero 
                                    { 
                                        IdPelicula = idPeliculaInsertada,
                                        IdGenero = item
                                    }
                                );
                        }
                        await _context.PeliculaGeneros.AddRangeAsync(relacionPeliculaGenero);
                        await _context.SaveChangesAsync();
                        
                        await trasn.CommitAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (System.Exception)
                    {
                        trasn.Rollback();
                    }
                    
                }// End Transacion
                */
            }
            else{
                
            }
            full.ListGeneros = await GetAllGeneros();
            return View(full);
        }

        // GET: PeliculaRepositories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peliculaRepository = await _context.PeliculaRepositories.FindAsync(id);
            if (peliculaRepository == null)
            {
                return NotFound();
            }
            return View(peliculaRepository);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPelicula,Titulo,Duracion,Descripcion,LinkPelicula,LinkImagen")] PeliculaRepository peliculaRepository)
        {
            if (id != peliculaRepository.IdPelicula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(peliculaRepository);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaRepositoryExists(peliculaRepository.IdPelicula))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(peliculaRepository);
        }

        // GET: PeliculaRepositories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peliculaRepository = await _context.PeliculaRepositories
                .FirstOrDefaultAsync(m => m.IdPelicula == id);
            if (peliculaRepository == null)
            {
                return NotFound();
            }

            return View(peliculaRepository);
        }

        // POST: PeliculaRepositories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var peliculaRepository = await _context.PeliculaRepositories.FindAsync(id);
            _context.PeliculaRepositories.Remove(peliculaRepository);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaRepositoryExists(int id)
        {
            return _context.PeliculaRepositories.Any(e => e.IdPelicula == id);
        }


        //======================== Recursos para cuando ocurra un Error======================
        [Route("404")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        //======================= Validaciones Remotas ==================================
        public JsonResult ValidaNombreRemoto(string ListaActores)
        {
            return Json(FormatoCorrecto(ListaActores));
        }
       
        private List<string> SplitFullList(string cadena)
        {
            List<string> retorno = new List<string>(); 

            var partes = cadena.Split(",");
            foreach (var item in partes)
            {
                if (item != "")
                {
                    retorno.Add(item);
                }
            }
            return retorno;
        }
        private bool FormatoCorrecto(string cadena)
        {
            bool bandera = true;
            var partes = cadena.Split(",");
            foreach (var item in partes)
            {
                item.Trim();
                if(item == "")
                {
                    bandera = false;
                    break;
                }
                else
                {
                    if (!Regex.IsMatch(item, "^[a-zA-Z ]*$") || item.Length < 5)
                    {
                        bandera = false;
                        break;
                    }
                }
            }
            return  bandera;
        }
    }
}
