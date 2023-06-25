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
    public class ComprasController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly INotyfService _notifyService;

        public ComprasController(GestionInventarioContext context, INotyfService notify)
        {
            _context = context;
            _notifyService = notify;
        }

        // GET: Compras
        [HttpGet("/Compras")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var gestionInventarioContext = _context.Compras.Include(c => c.IdUsuarioNavigation);
            return View(await gestionInventarioContext.ToListAsync());
        }

        // GET: Compras/Details/5
        [HttpGet("/Compras/Detalles")]
        [Authorize(Roles = "Administrador")]        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                _notifyService.Error("Datos invalidos!");
                return RedirectToAction(nameof(Index));
            }

            var compra = await _context.Compras
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compra == null)
            {
                _notifyService.Error("No encontramos la orden de compra!");
                return RedirectToAction(nameof(Index));
            }

            return View(compra);
        }

        [HttpGet("/Compras/Nueva")]
        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Compras/Nueva")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,IdUsuario")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                _notifyService.Success("Orden de compra generada con exito!");
                return RedirectToAction(nameof(Index));
            }
            _notifyService.Warning("No pudimos generar la orden de compra!");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombre", compra.IdUsuario);
            return View(compra);
        }

        // GET: Compras/Edit/5
        [HttpGet("/Compras/Editar/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                _notifyService.Error("Datos invalidos!");
                return RedirectToAction("Index");
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                _notifyService.Error("No encontramos la orden de compra!");
                return RedirectToAction("Index");
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombre", compra.IdUsuario);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Compras/Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,IdUsuario")] Compra compra)
        {
            if (id != compra.Id)
            {
                _notifyService.Error("Datos invalidos!");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Orden de compra modificada con exito!");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!CompraExists(compra.Id))
                    {
                        _notifyService.Error("No encontramos la orden de compra!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _notifyService.Error("Error: "+ex.Message);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _notifyService.Warning("Datos invalidos!");
            }
            return RedirectToAction("Details", new { id = id });
        }

        // GET: Compras/Delete/5
        [HttpGet("/Compras/Borrar/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                _notifyService.Error("Datos invalidos!");
                return RedirectToAction(nameof(Index));
            }

            var compra = await _context.Compras
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compra == null)
            {
                _notifyService.Error("No encontramos la orden de compra!");
                return RedirectToAction(nameof(Index));
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost("/Compras/Borrar"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Compras == null)
            {
                _notifyService.Error("Error en la base de datos!");
                return RedirectToAction(nameof(Index));
            }
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
                await _context.SaveChangesAsync();
                _notifyService.Success("Orden de compra eliminada con exito!");
            }
            else
            {
                _notifyService.Error("No encontramos la orden de compra!");
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
          return (_context.Compras?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
