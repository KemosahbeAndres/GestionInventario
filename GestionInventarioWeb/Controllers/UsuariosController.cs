using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionInventarioWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestionInventarioWeb.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly UsersFinder _usersFinder;

        public UsuariosController(GestionInventarioContext context)
        {
            _context = context;
            _usersFinder = new UsersFinder(context);
        }

        [HttpGet("/Users", Name = "Users")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            return View(_usersFinder.FindAll());
        }

        [Route("/Users/{id}")]
        [HttpGet, ActionName("Details")]
        [Authorize(Roles = "Administrador")]
        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpContext.Session.SetString("message", "");
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpGet("/Users/Create")]
        [Authorize(Roles = "Administrador")]
        // GET: Usuarios/Create
        public IActionResult Create()
        {
            HttpContext.Session.SetString("message", "");
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "Rol");
            return View("Views/Usuarios/Create.cshtml");
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Users/CreateNew"), ActionName("CreateNew")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateNew([Bind("Id,Nombre,Telefono,Rut,Clave,IdRol")] Usuario usuario)
        {
            HttpContext.Session.SetString("message", "");
            if (ModelState.IsValid)
            {
                try
                {
                    if (!RunValidator.Validar(usuario.Rut)) throw new Exception("Rut invalido");
                    _context.Usuarios.Add(usuario);
                    _context.SaveChanges();
                    HttpContext.Session.SetString("message", "Usuario creado con exito!");
                    //throw new Exception("Llego: " + name + " | " + username + " | " + Convert.ToString(rolid) + " | ");
                    return LocalRedirect("/Users");
                }
                catch (Exception ex)
                {
                    HttpContext.Session.SetString("message", "Error " + ex.Message);
                }
            }

            //ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "Id", usuario.IdRol);
            return LocalRedirect("/Users/Create");
        }

        [Route("/Users/Edit/{id}")]
        [HttpGet, ActionName("Edit")]
        [Authorize(Roles = "Administrador")]
        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            HttpContext.Session.SetString("message", "");
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "Rol", usuario.IdRol);
            return View("Edit",usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/Users/Save")]
        [HttpPost, ActionName("SaveUser")]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUser(int id, [Bind("Id,Nombre,Telefono,Rut,Clave,IdRol")] Usuario usuario)
        {
            HttpContext.Session.SetString("message", "");
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!RunValidator.Validar(usuario.Rut)) throw new Exception("Rut invalido");
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }catch(Exception ex)
                {
                    HttpContext.Session.SetString("message", ex.Message);
                    ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "Rol", usuario.IdRol);
                    return View("Edit", usuario);
                }
                //return LocalRedirect("/Users/"+id);
                HttpContext.Session.SetString("message", "Usuario guardado con exito!");
            }
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "Rol", usuario.IdRol);
            return View("Edit",usuario);
        }

        [Route("/Users/Delete/{id}")]
        [HttpGet, ActionName("CanDelete")]
        [Authorize(Roles = "Administrador")]
        // GET: Usuarios/Delete/5
        public async Task<IActionResult> CanDelete(int? id)
        {
            HttpContext.Session.SetString("message", "");
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View("Delete",usuario);
        }

        [Route("/Users/DeleteConfirmed")]
        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpContext.Session.SetString("message", "");
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'GestionInventarioContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
