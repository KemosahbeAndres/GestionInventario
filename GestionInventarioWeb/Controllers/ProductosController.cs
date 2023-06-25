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
    public class ProductosController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly INotyfService _notifyService;
        private readonly ProductsFinder _productsFinder;

        public ProductosController(GestionInventarioContext context, INotyfService notify)
        {
            _context = context;
            _notifyService = notify;
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
            if (id == null || _context.Productos == null)
            {
                _notifyService.Error("Datos ingresados incorrectos!");
                return RedirectToAction("Index");
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                _notifyService.Error("Producto no encontrado!");
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        [HttpGet("/Productos/Create"), ActionName("Create")]
        [Authorize(Roles = "Administrador,Vendedor")]
        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Categoria1");
            return View("Create");
        }

        [HttpGet("/Productos/Update/{id}"), ActionName("UpdateProduct")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> UpdateSelected(int? id)
        {
            var pr = await _context.Productos.FindAsync(id);
            try
            {
                if (pr == null)
                {
                    _notifyService.Error("Producto no encontrado!");
                    return RedirectToAction("Index");
                }
                ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Categoria1", pr.IdCategoria);
            }catch(Exception ex)
            {
                _notifyService.Error(ex.Message);
                ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Categoria1");
            }
            return View("Create", pr);
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Productos/CreateNew"), ActionName("CreateNew")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Vendedor")]
        public async Task<IActionResult> CreateNew([Bind("Id,Ean,Nombre,Descripcion,Precio,IdCategoria")] Producto producto, int stock = 1)
        {
         
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
                    _notifyService.Success("Producto creado con exito!");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _notifyService.Error("No pudimos crear el producto "+ producto.Nombre);
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

        [HttpGet("/Productos/Edit/{id}"), ActionName("Edit")]
        [Authorize(Roles = "Administrador, Vendedor")]
        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                _notifyService.Error("Datos ingresados incorrectos!");
                return RedirectToAction("Index");
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                _notifyService.Error("Producto no encontrado!");
                return RedirectToAction("Index");
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
            if (id != producto.Id)
            {
                _notifyService.Error("Los datos ingresados no concuerdan!");
                return RedirectToAction("Index");
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
                    _notifyService.Error("Error. " + ex.Message);
                    ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Categoria1", producto.IdCategoria);
                    //return View("Edit", producto);
                    return RedirectToAction("Edit", new { id = producto.Id });
                }
                //return RedirectToAction(nameof(Index));
                _notifyService.Success("Producto guardado con exito!");
            }
            //ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Categoria1", producto.IdCategoria);
            //return View("Edit", producto);
            return RedirectToAction("Edit", new { id = producto.Id });
        }

        [Route("/Productos/Delete/{id}")]
        [HttpGet, ActionName("CanDelete")]
        [Authorize(Roles = "Administrador, Vendedor")]
        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                _notifyService.Error("Los datos enviados son incorrectos!");
                return RedirectToAction("Index");
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                _notifyService.Error("Producto no encontrado!");
                return RedirectToAction("Index");
            }

            return View("Delete",producto);
        }

        [Route("/Productos/DeleteConfirmed")]
        // POST: Productos/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [Authorize(Roles = "Administrador, Vendedor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Productos == null)
                {
                    _notifyService.Error("Error en la base de datos. La tabla productos no se encontro!");
                    return RedirectToAction("Delete", new { id = id });
                }
                var producto = await _context.Productos.FindAsync(id);
                if (producto != null)
                {
                    _context.Productos.Remove(producto);
                }
            
                await _context.SaveChangesAsync();
                _notifyService.Success("Producto eliminado con exito!");

            }
            catch (Exception ex)
            {
                _notifyService.Error("No puedes eliminar este producto! Hay registros de venta que lo usan!");
                return RedirectToAction("Details", new { id = id });
            }
            return RedirectToAction("Index");
        }

        private bool ProductoExists(int id)
        {
          return (_context.Productos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
