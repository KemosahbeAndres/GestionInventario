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
        private readonly BuysFinder _buysFinder;

        public ComprasController(GestionInventarioContext context, INotyfService notify)
        {
            _context = context;
            _notifyService = notify;
            _buysFinder = new BuysFinder(_context);
        }

        // GET: Compras
        [HttpGet("/Compras")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _buysFinder.FindAllAsync());
        }

        // GET: Compras/Details/5
        [HttpGet("/Compras/Detalles"), ActionName("Details")]
        [Authorize(Roles = "Administrador")]        
        public async Task<IActionResult> Details(int id = 0)
        {
            if (_context.Compras == null)
            {
                _notifyService.Error("Datos invalidos!");
                return RedirectToAction(nameof(Index));
            }

            var compra = await _buysFinder.Find(id);
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
        [HttpPost("/Compras/Editando"), ActionName("UpdateBuy")]
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
        [HttpPost("/Compras/Borrar"), ActionName("DeleteConfirmed")]
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
                var items = await _context.ItemCompras.Where(i => i.IdCompra == compra.Id).ToListAsync();
                foreach (var item in items)
                {
                    //Inventario
                    var inv = await getLastInventory(item.IdProducto);
                    inv.Cantidad -= item.Cantidad;
                    _context.Inventarios.Update(inv);
                    _context.ItemCompras.Remove(item);
                }
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

        [HttpPost("/Compras/Details/AddProduct"), ActionName("BuyAddProduct")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> AddProduct(int id, int pid = 0, int cantidad = 1)
        {
            try
            {
                var inv = await getLastInventory(pid);
                if (await _buysFinder.HasProduct(id, pid))
                {
                    var item = await _context.ItemCompras.SingleOrDefaultAsync(i => i.IdCompra == id && i.IdProducto == pid);
                    item.Cantidad += cantidad;
                    _context.ItemCompras.Update(item);
                    //Inventario
                    inv.Cantidad += cantidad;
                    _context.Inventarios.Update(inv);

                    _context.SaveChanges();
                    _notifyService.Success("Producto encontrado, sumando");
                }
                else
                {
                    var item = new ItemCompra();
                    item.IdCompra = id;
                    item.IdProducto = pid;
                    item.Cantidad = cantidad;
                    _context.ItemCompras.Add(item);
                    //Inventario
                    inv.Cantidad += cantidad;
                    _context.Inventarios.Update(inv);

                    _context.SaveChanges();
                    _notifyService.Success("Producto agregado!");
                }
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error: " + ex.Message);
            }
            return RedirectToAction("Edit", new { id = id });
        }

        [HttpPost("/Compras/Details/PopProduct"), ActionName("BuyPopProduct")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> DropProduct(int id, int pid)
        {
            try
            {
                if (await _buysFinder.HasProduct(id, pid))
                {
                    var item = await _context.ItemCompras.SingleOrDefaultAsync(i => i.IdCompra == id && i.IdProducto == pid);
                    if (item == null) throw new Exception("No se encuentra el producto!");

                    //Inventario
                    var inv = await getLastInventory(pid);
                    inv.Cantidad -= item.Cantidad;
                    if(inv.Cantidad < 0)
                    {
                        inv.Cantidad = 0;
                    }
                    _context.ItemCompras.Remove(item);
                    _context.Inventarios.Update(inv);

                    _context.SaveChanges();
                    _notifyService.Success("Producto eliminado con exito!");
                }
            }
            catch (Exception ex)
            {
                _notifyService.Error("No logramos eliminar el producto! " + ex.Message);
            }
            return RedirectToAction("Edit", new { id = id });
        }

        private bool CompraExists(int id)
        {
          return (_context.Compras?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<Inventario?> getLastInventory(int productid)
        {
            return await _context.Inventarios.OrderBy(i => i.Id).LastOrDefaultAsync(i => i.IdProducto == productid);
        }
    }
}
