using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionInventarioWeb.Models;
using Microsoft.AspNetCore.Authorization;
using GestionInventarioWeb.Data;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using static iTextSharp.text.pdf.AcroFields;
using System.Security.Cryptography;

namespace GestionInventarioWeb.Controllers
{
    public class VentasController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly SalesFinder _salesFinder;
        private readonly ProductsFinder _productsFinder;

        public VentasController(GestionInventarioContext context)
        {
            _context = context;
            _salesFinder = new SalesFinder(_context);
            _productsFinder = new ProductsFinder(_context);
        }

        // GET: Ventas
        [HttpGet("/Ventas"), ActionName("Index")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Index()
        {
            var sales = await _salesFinder.FindAllAsync();
            return View(sales);
        }

        // GET: Ventas/Details/5
        [HttpGet("/Ventas/Detalles/{id}"), ActionName("Details")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Details(int? id)
        {
            int mid = 0;
            if (id == null)
            {
                return NotFound();
            }else
            {
                mid = (int)id;
            }

            var venta = await _salesFinder.Find(mid);

            if (venta == null)
            {
                return RedirectToAction("Index");
            }
            return View(venta);
        }

        // GET: Ventas/Create
        [HttpGet("/Ventas/Nueva"), ActionName("CreateSale")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public IActionResult Create()
        {
            var user = GetLoggedUser();
            int id = 0;
            if(user != null)
            {
                id = user.Id;
            }
            ViewData["IdVendedor"] = new SelectList(_context.Usuarios, "Id", "Nombre", id);

            return View("Create");
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Ventas/ConfirmNueva"), ActionName("CreateSaleConfirm")]
        [Authorize(Roles = "Administrador,Vendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,IdVendedor")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction("EditSale", new { id = venta.Id });
            }

            return LocalRedirect("/Ventas/Detalles/"+venta.Id);
        }

        // GET: Ventas/Edit/5
        [HttpGet("/Ventas/Edit/{id}"), ActionName("EditSale")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["IdVendedor"] = new SelectList(_context.Usuarios, "Id", "Nombre", venta.IdVendedor);
            return View("Edit",venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Ventas/Editting"), ActionName("EdittingSale")]
        [Authorize(Roles = "Administrador,Vendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,IdVendedor")] Venta venta)
        {
            if (id != venta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.Id))
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

            return RedirectToAction("Details", new { id = id });
            //return LocalRedirect("/Ventas/Detalles/"+venta.Id);
        }

        // GET: Ventas/Delete/5
        [HttpGet("/Ventas/Delete/{id}"), ActionName("DeleteSale")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.IdVendedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View("Delete", venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost("/Ventas/Deleting"), ActionName("DeletingSale")]
        [Authorize(Roles = "Administrador,Vendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ventas == null)
            {
                return Problem("Entity set 'GestionInventarioContext.Ventas'  is null.");
            }
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
                var items = await _context.ItemVenta.Where(i => i.IdVenta == venta.Id).ToListAsync();
                foreach(var item in items)
                {
                    //Inventario
                    var inv = await _context.Inventarios.OrderBy(i => i.Id).LastOrDefaultAsync(i => i.IdProducto == item.IdProducto);
                    inv.Cantidad += item.Cantidad;
                    _context.Inventarios.Update(inv);
                    _context.ItemVenta.Remove(item);
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/api/Productos"), ActionName("GetAllProducts")]
        public async Task<IActionResult> GetProducts(int id)
        {
            return Json(await _productsFinder.FindAllAsync());
        }

        [HttpGet("/api/Productos/Venta/{id}")]
        public async Task<IActionResult> GetProductsFromSale(int id)
        {
            return Json(await _productsFinder.FindBySale(id));
        }

        [HttpGet("/api/Ventas")]
        public async Task<IActionResult> GetSales()
        {
            var ventas = await _context.Ventas.Include(s => s.IdVendedorNavigation).ToListAsync();
            var sales = new List<Object>();
            foreach(var venta in ventas)
            {
                var total = 0;
                var products = await _productsFinder.FindBySale(venta.Id);
                foreach (var product in products)
                {
                    total += product.Price * product.Cantidad;
                }
                sales.Add(new {
                    id = venta.Id,
                    date = venta.Fecha.ToShortDateString(),
                    seller = venta.IdVendedorNavigation.Nombre,
                    cost = total
                });
            }
            return Json(sales.ToArray());
        }

        [HttpPost("/Ventas/Details/AddProduct"), ActionName("SaleAddProduct")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> AddProduct(int id, int pid, int cantidad = 1)
        {
            var mensaje = "";
            try
            {
                if(await _salesFinder.HasProduct(id, pid))
                {
                    var item = await _context.ItemVenta.SingleOrDefaultAsync(i => i.IdVenta == id && i.IdProducto == pid);
                    item.Cantidad += cantidad;
                    _context.ItemVenta.Update(item);
                    //Inventario
                    var inv = await _context.Inventarios.OrderBy(i => i.Id).LastOrDefaultAsync(i => i.IdProducto == pid);
                    inv.Cantidad -= cantidad;
                    _context.Inventarios.Update(inv);

                    _context.SaveChanges();
                    mensaje = "Producto encontrado, sumando";
                }
                else
                {
                    var item = new ItemVentum();
                    item.IdVenta = id;
                    item.IdProducto = pid;
                    item.Cantidad = cantidad;
                    _context.ItemVenta.Add(item);
                    //Inventario
                    var inv = await _context.Inventarios.OrderBy(i => i.Id).LastOrDefaultAsync(i => i.IdProducto == pid);
                    inv.Cantidad -= cantidad;
                    _context.Inventarios.Update(inv);

                    _context.SaveChanges();
                    mensaje = "Producto agregado!";
                }
            }catch(Exception ex)
            {
                HttpContext.Session.SetString("error", ex.Message);
            }
            HttpContext.Session.SetString("message", mensaje);
            return RedirectToAction("EditSale", new { id = id });
        }

        [HttpPost("/Ventas/Details/PopProduct"), ActionName("SalePopProduct")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> DropProduct(int id, int pid)
        {
            try
            {
                if(await _salesFinder.HasProduct(id, pid))
                {
                    var item = await _context.ItemVenta.SingleOrDefaultAsync(i => i.IdVenta == id && i.IdProducto == pid);
                    if (item == null) throw new Exception("No se encuentra el producto!");
                    _context.ItemVenta.Remove(item);
                    //Inventario
                    var inv = await _context.Inventarios.OrderBy(i => i.Id).LastOrDefaultAsync(i => i.IdProducto == pid);
                    inv.Cantidad += item.Cantidad;
                    _context.Inventarios.Update(inv);

                    _context.SaveChanges();
                }
            }catch(Exception ex)
            {
                HttpContext.Session.SetString("error", "No logramos eliminar el producto! \n"+ ex.Message);
            }
            return RedirectToAction("EditSale", new { id = id });
        }

        private bool VentaExists(int id)
        {
          return (_context.Ventas?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private User? GetLoggedUser()
        {
            var claims = HttpContext.User.Claims.ToList();
            string value = claims.FirstOrDefault(c => c.Type.Contains("Rut")).Value;
            if (claims.Count <= 0)
            {
                return null;
            }
            var user = _context.Usuarios.SingleOrDefault(u => u.Rut.Equals(value));

            if (user == null)
            {

                return null;
            }
            var role = _context.Roles.SingleOrDefault(r => r.Id.Equals(user.IdRol));

            string phone = user.Telefono == null ? "" : user.Telefono;

            return new User(user.Id, user.Nombre, user.Rut, phone, role.Rol);
        }
    }
}
