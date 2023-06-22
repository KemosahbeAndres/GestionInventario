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

namespace GestionInventarioWeb.Controllers
{
    public class VentasController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly SalesFinder _salesFinder;

        public VentasController(GestionInventarioContext context)
        {
            _context = context;
            _salesFinder = new SalesFinder(_context);
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
                return RedirectToAction(nameof(Index));
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
            ViewData["IdVendedor"] = new SelectList(_context.Usuarios, "Id", "Id", venta.IdVendedor);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Ventas/Editting/{id}"), ActionName("EdittingSale")]
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

            return LocalRedirect("/Ventas/Detalles/"+venta.Id);
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

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost("/Ventas/Deleting/{id}"), ActionName("DeletingSale")]
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
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("/Ventas/Details/{id}/AddProduct/{pid}"), ActionName("SaleAddProduct")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> AddProduct(int id, int pid)
        {
            return RedirectToAction("Details");
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
