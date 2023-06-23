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

        [HttpGet("/Productos", Name = "Productos"), ActionName("Index")]
        [Authorize(Roles = "Administrador, Vendedor")]
        public async Task<IActionResult> Index()
        {
            return View(await _productsFinder.FindAllAsync());
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

        [HttpGet("/Productos/Create"), ActionName("Create")]
        [Authorize(Roles = "Administrador, Vendedor")]
        // GET: Productos/Create
        public IActionResult Create()
        {
            HttpContext.Session.SetString("message", "");
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Categoria1");
            return View("Create");
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Productos/CreateNew"), ActionName("CreateNew")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Vendedor")]
        public async Task<IActionResult> CreateNew([Bind("Id,Ean,Nombre,Descripcion,Precio,IdCategoria")] Producto producto, int stock = 1)
        {
            HttpContext.Session.SetString("message", "");
            HttpContext.Session.SetString("error", "");
            if (ModelState.IsValid)
            {
                try
                {
                    int count = await _context.Productos.CountAsync() > 0
                        ? await LastProductId()
                        : 0;
                    producto.Ean = BarCodeController.generate13(count);
                    _context.Add(producto);
                    _context.SaveChanges();
                    // Inventario
                    var inv = new Inventario();
                    inv.IdProducto = await LastProductId();
                    inv.Cantidad = stock;
                    inv.Fecha = DateTime.Now;

                    _context.Add(inv);
                    _context.SaveChanges();
                    HttpContext.Session.SetString("message", "Producto creado con exito.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    HttpContext.Session.SetString("message", "Error." + ex.Message);
                }
            }

            //ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Id", producto.IdCategoria);
            return RedirectToAction("Create");
        }

        private async Task<int> LastProductId()
        {
            var p = await _context.Productos.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
            return p.Id;
        }

        [HttpGet("/Inventario/{id}")]
        public async Task<IActionResult> getInventory(int id)
        {
            int stock = 1;
            try
            {
                var inv = await _context.Inventarios.OrderBy(i => i.Id).LastOrDefaultAsync(i => i.IdProducto == id);
                stock = inv.Cantidad;
            }catch(Exception ex)
            {
                stock = 1;
            }
            return Json(new { stock = stock });
        }

        [Route("/Productos/Edit/{id}")]
        [HttpGet, ActionName("Edit")]
        [Authorize(Roles = "Administrador, Vendedor")]
        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            HttpContext.Session.SetString("message", "");
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Categoria1", producto.IdCategoria);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/Productos/Save")]
        [HttpPost, ActionName("SaveProduct")]
        [Authorize(Roles = "Administrador, Vendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveProduct(int id, [Bind("Id,Ean,Nombre,Descripcion,Precio,IdCategoria")] Producto producto, int stock)
        {
            HttpContext.Session.SetString("message", "");
            HttpContext.Session.SetString("error", "");
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && stock > 0)
            {
                try
                {
                    _context.Update(producto);
                    //await _context.SaveChangesAsync();
                    // Inventario
                    var inv = await _context.Inventarios.OrderBy(i => i.Id).LastOrDefaultAsync(i => i.IdProducto == producto.Id);
                    if (inv == null)
                    {
                        inv = new Inventario();
                        inv.IdProducto = producto.Id;
                        inv.Fecha = DateTime.Now;
                        inv.Cantidad = stock;
                        _context.Inventarios.Add(inv);
                    }
                    else
                    {
                        inv.Cantidad = stock;
                        _context.Inventarios.Update(inv);
                    }

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
                }catch (Exception ex)
                {
                    HttpContext.Session.SetString("message", ex.Message);
                    ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Categoria1", producto.IdCategoria);
                    return View("Edit", producto);
                }
                //return RedirectToAction(nameof(Index));
                HttpContext.Session.SetString("message", "Producto guardado con exito.");
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Categoria1", producto.IdCategoria);
            return View("Edit", producto);
            //return RedirectToAction("Edit", new { id = producto.Id });
        }

        [Route("/Productos/Delete/{id}")]
        [HttpGet, ActionName("CanDelete")]
        [Authorize(Roles = "Administrador, Vendedor")]
        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            HttpContext.Session.SetString("message", "");
            HttpContext.Session.SetString("error", "");
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

            return View("Delete",producto);
        }

        [Route("/Productos/DeleteConfirmed")]
        // POST: Productos/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [Authorize(Roles = "Administrador, Vendedor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpContext.Session.SetString("message", "");
            try
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

            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("error", "No puedes eliminar este producto! Hay registros de venta que lo usan!");
            }

            return RedirectToAction("Index");
        }

        private bool ProductoExists(int id)
        {
          return (_context.Productos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
