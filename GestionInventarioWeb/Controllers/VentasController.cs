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
using AspNetCoreHero.ToastNotification.Abstractions;

namespace GestionInventarioWeb.Controllers
{
    public class VentasController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly INotyfService _notifyService;
        private readonly SalesFinder _salesFinder;
        private readonly ProductsFinder _productsFinder;

        public VentasController(GestionInventarioContext context, INotyfService notify)
        {
            _context = context;
            _notifyService = notify;
            _salesFinder = new SalesFinder(_context);
            _productsFinder = new ProductsFinder(_context);
        }

        // GET: Ventas
        [HttpGet("/Ventas"), ActionName("Index")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Index()
        {
            return View(await _salesFinder.FindAllAsync());
        }

        // GET: Ventas/Details/5
        [HttpGet("/Ventas/Detalles/{id}"), ActionName("Details")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Details(int id = 0)
        {
            if (id == 0)
            {
                _notifyService.Error("Datos invalidos!");
                return RedirectToAction("Index");
            }

            var venta = await _salesFinder.Find(id);

            if (venta == null)
            {
                _notifyService.Error("No encontramos el registro de venta!");
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
                _notifyService.Success("Venta creada con exito!");
                return RedirectToAction("EditSale", new { id = venta.Id });
            }
            else
            {
                _notifyService.Warning("Datos invalidos!");
            }

            return RedirectToAction("Details", new { id = venta.Id });
        }

        // GET: Ventas/Edit/5
        [HttpGet("/Ventas/Edit/{id}"), ActionName("EditSale")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                _notifyService.Error("Datos invalidos!");
                return RedirectToAction("Index");
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                _notifyService.Error("Venta no encontrada!");
                return RedirectToAction("Index");
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
                _notifyService.Error("Datos invalidos!");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Venta guardada con exito!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.Id))
                    {
                        _notifyService.Error("Venta no encontrada!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _notifyService.Error("Ocurrio un error inesperado al conectar con la base de datos!");
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                _notifyService.Error("Datos invalidos!");
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
                _notifyService.Error("Datos invalidos!");
                return RedirectToAction("Index");
            }

            var venta = await _context.Ventas
                .Include(v => v.IdVendedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venta == null)
            {
                _notifyService.Error("No encontramos la venta!");
                return RedirectToAction("Index");
            }

            return View("Delete", venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost("/Ventas/Deleting"), ActionName("DeletingSale")]
        [Authorize(Roles = "Administrador,Vendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id = 0)
        {
            if (_context.Ventas == null)
            {
                _notifyService.Error("Ocurrio un error inesperado con la base de datos!");
                return RedirectToAction("Index");
            }
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                var items = await _context.ItemVenta.Where(i => i.IdVenta == venta.Id).ToListAsync();
                foreach(var item in items)
                {
                    //Inventario
                    var inv = await getLastInventory(item.IdProducto);
                    inv.Cantidad += item.Cantidad;
                    _context.Inventarios.Update(inv);
                    _context.ItemVenta.Remove(item);
                }
                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
                _notifyService.Success("Venta guardada con exito!");
            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("/Ventas/Details/AddProduct"), ActionName("SaleAddProduct")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> AddProduct(int id, int pid = 0, int cantidad = 1)
        {
            try
            {
                var inv = await getLastInventory(pid);
                if(await _salesFinder.HasProduct(id, pid))
                {
                    var item = await _context.ItemVenta.SingleOrDefaultAsync(i => i.IdVenta == id && i.IdProducto == pid);
                    item.Cantidad += cantidad;
                    _context.ItemVenta.Update(item);
                    //Inventario
                    inv.Cantidad -= cantidad;
                    if (inv.Cantidad < 0)
                    {
                        inv.Cantidad = 0;
                    }
                    _context.Inventarios.Update(inv);

                    _context.SaveChanges();
                    _notifyService.Success("Producto encontrado, sumando");
                }
                else
                {
                    var item = new ItemVentum();
                    item.IdVenta = id;
                    item.IdProducto = pid;
                    item.Cantidad = cantidad;
                    _context.ItemVenta.Add(item);
                    //Inventario
                    inv.Cantidad -= cantidad;
                    if (inv.Cantidad < 0)
                    {
                        inv.Cantidad = 0;
                    }
                    _context.Inventarios.Update(inv);

                    _context.SaveChanges();
                    _notifyService.Success("Producto agregado!");
                }
            }catch(Exception ex)
            {
                _notifyService.Error("Error: "+ ex.Message);
            }
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
                    
                    //Inventario
                    var inv = await getLastInventory(pid);
                    inv.Cantidad += item.Cantidad;
                    _context.ItemVenta.Remove(item);
                    _context.Inventarios.Update(inv);

                    _context.SaveChanges();
                    _notifyService.Success("Producto eliminado con exito!");
                }
            }catch(Exception ex)
            {
                _notifyService.Error("No logramos eliminar el producto! "+ ex.Message);
            }
            return RedirectToAction("EditSale", new { id = id });
        }

        private bool VentaExists(int id)
        {
          return (_context.Ventas?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<Inventario?> getLastInventory(int productid)
        {
            return await _context.Inventarios.OrderBy(i => i.Id).LastOrDefaultAsync(i => i.IdProducto == productid);
        }

        private User? GetLoggedUser()
        {
            var claims = HttpContext.User.Claims.ToList();
            var claim = claims.FirstOrDefault(c => c.Type.Contains("Rut"));
            if (claim == null) return null;
            string value = claim.Value;
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
