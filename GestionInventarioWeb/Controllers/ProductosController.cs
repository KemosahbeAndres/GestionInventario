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
    public class ProductosController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly ProductsFinder _productsFinder;

        public ProductosController(GestionInventarioContext context)
        {
            _context = context;
            _productsFinder = new ProductsFinder(context);
        }

        [HttpGet("/Productos", Name = "Productos")]
        [Authorize(Roles = "Administrador, Vendedor")]
        public async Task<IActionResult> Index()
        {
            return View(_productsFinder.FindAll());
        }

        [Route("/Productos/{id}")]
        [HttpGet, ActionName("Details")]
        [Authorize(Roles = "Administrador, Vendedor")]
        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpContext.Session.SetString("message", "");
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [HttpGet("/Productos/Create")]
        [Authorize(Roles = "Administrador, Vendedor")]
        // GET: Productos/Create
        public IActionResult Create()
        {
            HttpContext.Session.SetString("message", "");
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Id");
            return View("Views/Productos/Create.cshtml");
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Productos/CreateNew"), ActionName("CreateNew")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Vendedor")]
        public async Task<IActionResult> Create([Bind("Id,Ean,Nombre,Descripcion,Precio,IdCategoria")] Producto producto)
        {
            HttpContext.Session.SetString("message", "");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(producto);
                    _context.SaveChanges();
                    HttpContext.Session.SetString("message", "Producto creado con exito.");
                    return LocalRedirect("/Productos");
                }
                catch (Exception ex)
                {
                    HttpContext.Session.SetString("message", "Error." + ex.Message);
                }
            }

            //ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Id", producto.IdCategoria);
            return LocalRedirect("/Productos/Create");
        }

        [Route("/Productos/Edit/{id}")]
        [HttpGet, ActionName("Edit")]
        [Authorize(Roles = "Administrador, Vendedor")]
        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Id", producto.IdCategoria);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ean,Nombre,Descripcion,Precio,IdCategoria")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Id", producto.IdCategoria);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'GestionInventarioContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return (_context.Productos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
