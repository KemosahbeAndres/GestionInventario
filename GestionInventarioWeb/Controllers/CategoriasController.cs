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
    public class CategoriasController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly INotyfService _notifyService;

        public CategoriasController(GestionInventarioContext context, INotyfService notify)
        {
            _context = context;
            _notifyService = notify;
        }

        // GET: Categorias
        [HttpGet("/Categorias")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Index()
        {
              return _context.Categorias != null ? 
                          View(await _context.Categorias.ToListAsync()) :
                          Problem("Entity set 'GestionInventarioContext.Categorias'  is null.");
        }

        // GET: Categorias/Details/5
        [HttpGet("/Categorias/{id}")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                _notifyService.Error("Parametros invalidos!");
                return RedirectToAction("Index");
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                _notifyService.Error("No encontramos la categoria o no existe!");
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        [HttpGet("/Categorias/Create")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Categorias/CreateNew"), ActionName("CreateNewCategory")]
        [Authorize(Roles = "Administrador,Vendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Categoria1")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(categoria);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Categoria agregada con exito!");
                }catch(Exception ex)
                {
                    _notifyService.Error("Error al guardar la categoria!");
                }
                return RedirectToAction(nameof(Index));
            }
            _notifyService.Warning("Parametros invalidos!");
            return View("Create", categoria);
        }

        // GET: Categorias/Edit/5
        [HttpGet("/Categorias/Editar/{id}")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                _notifyService.Error("Parametros invalidos!");
                return RedirectToAction("Index");
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                _notifyService.Error("No encontramos la categoria!");
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Categorias/Editando"), ActionName("EdittingCategory")]
        [Authorize(Roles = "Administrador,Vendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Categoria1")] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                _notifyService.Error("Parametros invalidos!");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Categoria guardada!");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!CategoriaExists(categoria.Id))
                    {
                        _notifyService.Error("No encontramos la categoria!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _notifyService.Error(ex.Message);
                    }
                }

                return RedirectToAction("Details", new { id = id});
            }
            _notifyService.Warning("Parametros invalidos!");
            return View("Edit", categoria);
        }

        // GET: Categorias/Delete/5
        [HttpGet("/Categorias/Borrar/{id}")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                _notifyService.Error("Parametros invalidos!");
                return RedirectToAction("Index");
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                _notifyService.Error("Categoria no encontrada!");
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost("/Categorias/Borrando"), ActionName("DelettingCategory")]
        [Authorize(Roles = "Administrador,Vendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Categorias == null)
                {
                    _notifyService.Error("Error al conectar con base de datos!");
                    return RedirectToAction("Details", new { id = id});
                }
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria == null)
                {
                    _notifyService.Error("No encontramos la categoria!");
                    return RedirectToAction("Index");
                }
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                _notifyService.Success("Categoria eliminada con exito!");
            }catch(Exception ex)
            {
                _notifyService.Error("No puedes eliminar, hay productos usando esta categoria!");
                return RedirectToAction("Details", new { id = id });
            }
            return RedirectToAction("Index");
        }

        private bool CategoriaExists(int id)
        {
          return (_context.Categorias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
