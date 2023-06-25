using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionInventarioWeb.Models;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace GestionInventarioWeb.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly INotyfService _notifyService;
        private readonly UsersFinder _usersFinder;

        public UsuariosController(GestionInventarioContext context, INotyfService notify)
        {
            _context = context;
            _notifyService = notify;
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
            if (id == null || _context.Usuarios == null)
            {
                _notifyService.Error("Datos ingresados incorrectos!");
                return RedirectToAction("Index");
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                _notifyService.Error("Usuario no encontrado!");
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        [HttpGet("/Users/Create"), ActionName("Create")]
        [Authorize(Roles = "Administrador")]
        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "Rol");
            return View("Create");
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Users/CreateNew"), ActionName("CreateNew")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateNew([Bind("Id,Nombre,Telefono,Rut,Clave,IdRol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!RunValidator.Validar(usuario.Rut)) throw new Exception("Rut invalido");
                    if (await _context.Usuarios.SingleOrDefaultAsync(u => u.Rut.Equals(usuario.Rut)) != null) throw new Exception("El usuario ya existe!");
                    _context.Usuarios.Add(usuario);
                    _context.SaveChanges();
                    _notifyService.Success("Usuario creado con exito!");
                    //throw new Exception("Llego: " + name + " | " + username + " | " + Convert.ToString(rolid) + " | ");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _notifyService.Error("No pudimos crear al usuario "+ usuario.Nombre);
                }
            }
            else
            {
                _notifyService.Warning("Datos invalidos!");
            }

            //ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "Id", usuario.IdRol);
            return RedirectToAction("Create");
        }

        [Route("/Users/Edit/{id}")]
        [HttpGet, ActionName("Edit")]
        [Authorize(Roles = "Administrador")]
        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                _notifyService.Error("Datos ingresados invalidos!");
                return RedirectToAction("Index");
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                _notifyService.Error("Usuario no encontrado!");
                return RedirectToAction("Index");
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
        public async Task<IActionResult> SaveUser(int id, [Bind("Id,Nombre,Telefono,Rut,IdRol")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                _notifyService.Error("Los datos no concuerdan!");
                return RedirectToAction("Index");
            }
            ModelState.Remove("Clave");
            if (ModelState.IsValid)
            {
                try
                {
                    if (!RunValidator.Validar(usuario.Rut)) throw new Exception("Rut invalido");
                    var recover = await _context.Usuarios.FindAsync(usuario.Id);
                    if (recover == null) throw new Exception("Usuario no encontrado!");
                    recover.Rut = usuario.Rut;
                    recover.Nombre = usuario.Nombre;
                    recover.Telefono = usuario.Telefono;
                    if (!usuario.IdRol.Equals(recover.IdRol)) _notifyService.Information("Cambiaste el rol del usuario, la proxima vez este usuario inicie sesion sus permisos seran diferentes!", 12);
                    recover.IdRol = usuario.IdRol;

                    _context.Update(recover);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Usuario guardado con exito!");
                    return RedirectToAction("Details", new { id = recover.Id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        _notifyService.Error("Error en la base de datos, el usuario no se encontro!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _notifyService.Error("Usuario encontrado, pero error de concurrencia al actualizar!");
                        return RedirectToAction("Index");
                    }
                }catch(Exception ex)
                {
                    _notifyService.Error("Error: "+ex.Message);
                    //ViewData["IdRol"] = new SelectList(_context.Roles, "Id", "Rol", usuario.IdRol);
                    return RedirectToAction("Edit", new { id = usuario.Id });
                }
            }
            else
            {
                _notifyService.Warning("Datos invalidos!");
            }
            return RedirectToAction("Edit", new { id = usuario.Id });
        }

        [Route("/Users/Delete/{id}")]
        [HttpGet, ActionName("CanDelete")]
        [Authorize(Roles = "Administrador")]
        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                _notifyService.Error("Los datos no concuerdan!");
                return RedirectToAction("Index");
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                _notifyService.Error("Usuario no encontrado!");
                return RedirectToAction("Index");
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
            try
            {
                if (_context.Usuarios == null)
                {
                    _notifyService.Error("Error en la base de datos! La tabla usuarios no existe!");
                    return RedirectToAction("Index");
                }
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario != null)
                {
                    _context.Usuarios.Remove(usuario);
                }
                else
                {
                    _notifyService.Information("No se encontro ningun usuario para eliminar!");
                }
            
                await _context.SaveChangesAsync();

            }catch(DbUpdateException ex)
            {
                _notifyService.Error("No puedes eliminar un usuario con ventas registradas!");
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                _notifyService.Error(ex.Message);
                return RedirectToAction("Index");
            }
            _notifyService.Success("Usuario eliminado con exito!");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/Usuarios/CambiarClave/{id}"), ActionName("CambiarClave")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> changeStepTwo(int id = 0)
        {
            Usuario? user = null;
            try
            {
                if(!UsuarioExists(id)) throw new Exception("Usuario no encontrado!");
                user = await _context.Usuarios.FindAsync(id);
            }catch(Exception ex)
            {
                _notifyService.Error(ex.Message);
                return RedirectToAction("Index");
            }
            return View("ConfirmarCambio", user);
        }

        [HttpPost("/Usuarios/ConfirmarCambio"), ActionName("ConfirmarCambio")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> changeStepThree(int id, string password, string confirmpwd)
        {
            try
            {
                var user = await _context.Usuarios.FindAsync(id);
                if (user == null) throw new Exception("Ocurrio un problema, no encontramos al usuario!");
                if (!RunValidator.Validar(user.Rut)) throw new Exception("Rut invalido!");
                if (!password.Trim().Equals(confirmpwd.Trim()))
                {
                    _notifyService.Error("Las contraseñas no coinciden!");
                    return RedirectToAction("CambiarClave", new { id = id });
                }
                user.Clave = password.Trim();
                _context.Update(user);
                await _context.SaveChangesAsync();
                _notifyService.Success("Contraseña actualizada con exito!");
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message);
            }
            return RedirectToAction("Details", new { id = id });
        }

        private bool UsuarioExists(int id)  
        {
          return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
