using AspNetCoreHero.ToastNotification.Abstractions;
using GestionInventarioWeb.Data;
using GestionInventarioWeb.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventarioWeb.Controllers
{
    public class PdfController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly INotyfService _notifyService;
        private readonly SalesFinder _salesFinder;
        private readonly BuysFinder _buysFinder;
        private readonly ProductsFinder _productsFinder;
        private readonly UsersFinder _usersFinder;

        public PdfController(GestionInventarioContext context, INotyfService notify)
        {
            _context = context;
            _notifyService = notify;
            _salesFinder = new SalesFinder(_context);
            _buysFinder = new BuysFinder(_context);
            _productsFinder = new ProductsFinder(_context);
            _usersFinder = new UsersFinder(_context);
        }

        [HttpGet("/Reportes"), ActionName("Index")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var sales = await _salesFinder.FindAllAsync();
            var buys = await _buysFinder.FindAllAsync();
            var products = await _productsFinder.FindAllAsync();
            var users = _usersFinder.FindAll();

            Reports reportes = new Reports(
                sales.OrderByDescending(s => s.Date),
                buys.OrderByDescending(b => b.Date),
                products.Where(p => p.Stock <= 5),
                users
            );
            return View(reportes);
        }

        [HttpGet("/Reportes/Productos"), ActionName("NoStockProducts")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> renderProductsReport()
        {
            try
            {

                var stream = new MemoryStream();

                Document doc = new Document(PageSize.LETTER);

                var writer = PdfWriter.GetInstance(doc, stream);

                writer.CloseStream = false;

                doc.Open();

                doc.AddTitle("Reporte - Productos poco Stock");

                doc.Add(Title("Reporte"));
                doc.Add(Title("Productos poco Stock"));
                doc.Add(Space());

                var products = await _productsFinder.FindAllAsync();
                var filtered = products.Where(p => p.Stock <= 5);

                var table = BuildProductTable(filtered, true);

                doc.Add(table);

                doc.Close();

                writer.Flush();

                stream.Seek(0, SeekOrigin.Begin);  

                string filename = $"reporte_productos_{DateTime.Now}.pdf";

                var response = HttpContext.Response;
                response.ContentType = "application/pdf";
                response.Headers.Add("Content-Disposition", $"attachment; filename={filename}");

                //return new EmptyResult();

                var file = new FileStreamResult(stream, "application/pdf");

                file.FileDownloadName = filename;

                return file;

            }
            catch(Exception ex)
            {
                _notifyService.Error("Error al generar el reporte!");
            }
            return RedirectToAction("Index");

        }

        [HttpGet("/Reportes/Ventas/{months}"), ActionName("SalesReport")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> renderSalesReport(int months = 0)
        {
            try
            {

                var stream = new MemoryStream();

                Document doc = new Document(PageSize.LETTER);

                var writer = PdfWriter.GetInstance(doc, stream);

                writer.CloseStream = false;

                doc.Open();

                if(months > 0)
                {
                    doc.AddTitle($"Reporte - Ventas ultimos {months} meses");
                    doc.Add(Title("Reporte"));
                    doc.Add(Title($"Ventas ultimos {months} meses"));
                }
                else
                {
                    doc.AddTitle($"Reporte - Todas las Ventas");
                    doc.Add(Title("Reporte"));
                    doc.Add(Title($"Todas las Ventas"));
                }
                doc.Add(Space());

                var sales = await _salesFinder.FindAllAsync();
                var filtered = sales;
                if (months > 0)
                {
                    filtered = sales.Where(s => s.Date >= DateTime.Now.AddMonths(-months));
                }

                //doc.Add(Text("  ID   | EAN        | Producto      | Categoria    | Descripcion        | Existencias"));

                foreach (var s in filtered)
                {
                    doc.Add(Text($"ID: {s.Id}"));
                    doc.Add(Text($"Vendedor: {s.Seller.Name}"));
                    doc.Add(Text($"Fecha: {s.Date.ToShortDateString()}"));
                    doc.Add(Text($"Total: ${s.Cost}"));
                    doc.Add(Space());
                    var table = BuildProductTable(s.Products);
                    doc.Add(table);
                    doc.Add(Line());
                    //doc.Add(Text($" {p.Id} | {p.EAN} | {p.Name}  | {p.Category}  | {p.Description}   | {p.Stock}"));
                }
                //doc.Add(Line());

                doc.Close();

                writer.Flush();

                stream.Seek(0, SeekOrigin.Begin);


                string filename = $"reporte_ventas_{DateTime.Now}.pdf";

                var response = HttpContext.Response;
                response.ContentType = "application/pdf";
                response.Headers.Add("Content-Disposition", $"attachment; filename={filename}");

                //return new EmptyResult();

                var file = new FileStreamResult(stream, "application/pdf");

                file.FileDownloadName = filename;

                return file;
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error al generar el reporte!");
            }
            return RedirectToAction("Index");
        }
        
        [HttpGet("/Reportes/Compras/{months}"), ActionName("BuysReport")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> renderBuysReport(int months = 0)
        {
            try
            {

                var stream = new MemoryStream();

                Document doc = new Document(PageSize.LETTER);

                var writer = PdfWriter.GetInstance(doc, stream);

                writer.CloseStream = false;

                doc.Open();

                if (months > 0)
                {
                    doc.AddTitle($"Reporte - Compras ultimos {months} meses");
                    doc.Add(Title("Reporte"));
                    doc.Add(Title($"Compras ultimos {months} meses"));
                }
                else
                {
                    doc.AddTitle($"Reporte - Todas las Compras");
                    doc.Add(Title("Reporte"));
                    doc.Add(Title($"Todas las Compras"));
                }
                doc.Add(Space());

                var buys = await _buysFinder.FindAllAsync();
                var filtered = buys;
                if (months > 0)
                {
                    filtered = buys.Where(s => s.Date >= DateTime.Now.AddMonths(-months));
                }

                //doc.Add(Text("  ID   | EAN        | Producto      | Categoria    | Descripcion        | Existencias"));

                foreach (var s in filtered)
                {
                    doc.Add(Text($"ID: {s.Id}"));
                    doc.Add(Text($"Vendedor: {s.Buyer.Name}"));
                    doc.Add(Text($"Fecha: {s.Date.ToShortDateString()}"));
                    doc.Add(Text($"Total: ${s.Cost}"));
                    doc.Add(Space());
                    var table = BuildProductTable(s.Products);
                    doc.Add(table);
                    doc.Add(Line());
                    //doc.Add(Text($" {p.Id} | {p.EAN} | {p.Name}  | {p.Category}  | {p.Description}   | {p.Stock}"));
                }

                doc.Close();

                writer.Flush();

                stream.Seek(0, SeekOrigin.Begin);


                string filename = $"reporte_compras_{DateTime.Now}.pdf";

                var response = HttpContext.Response;
                response.ContentType = "application/pdf";
                response.Headers.Add("Content-Disposition", $"attachment; filename={filename}");

                //return new EmptyResult();

                var file = new FileStreamResult(stream, "application/pdf");

                file.FileDownloadName = filename;

                return file;
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error al generar el reporte!");
            }
            return RedirectToAction("Index");
        }

        [HttpGet("/Reportes/Usuario/{id}/{sale}"), ActionName("UserReport")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> renderUserReport(int id = 0, bool sale = true)
        {
            try
            {

                var stream = new MemoryStream();

                Document doc = new Document(PageSize.LETTER);

                var writer = PdfWriter.GetInstance(doc, stream);

                writer.CloseStream = false;

                doc.Open();

                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    return RedirectToAction("Index");
                }

                int count = 0;

                if (sale)
                {
                    var sales = await _salesFinder.FindAllAsync();
                    var fsales = sales.Where(s => s.Seller.Id == usuario.Id);
                    count = fsales.Count();
                    doc.AddTitle($"Reporte - Ventas {usuario.Nombre}");
                    doc.Add(Title("Reporte"));
                    doc.Add(Title($"Ventas {usuario.Nombre}"));
                    doc.Add(Space());
                    foreach (var s in fsales)
                    {
                        doc.Add(Text($"ID: {s.Id}"));
                        doc.Add(Text($"Vendedor: {s.Seller.Name}"));
                        doc.Add(Text($"Fecha: {s.Date.ToShortDateString()}"));
                        doc.Add(Text($"Total: ${s.Cost}"));
                        doc.Add(Space());
                        var table = BuildProductTable(s.Products);
                        doc.Add(table);
                        doc.Add(Line());
                    }
                }
                else
                {
                    var buys = await _buysFinder.FindAllAsync();
                    var fbuys = buys.Where(b => b.Buyer.Id == usuario.Id);
                    count = fbuys.Count();
                    doc.AddTitle($"Reporte - Compras {usuario.Nombre}");
                    doc.Add(Title("Reporte"));
                    doc.Add(Title($"Compra {usuario.Nombre}"));
                    doc.Add(Space());
                    foreach (var b in fbuys)
                    {
                        doc.Add(Text($"ID: {b.Id}"));
                        doc.Add(Text($"Vendedor: {b.Buyer.Name}"));
                        doc.Add(Text($"Fecha: {b.Date.ToShortDateString()}"));
                        doc.Add(Text($"Total: ${b.Cost}"));
                        doc.Add(Space());
                        var table = BuildProductTable(b.Products);
                        doc.Add(table);
                        doc.Add(Line());
                    }
                }

                if(count <= 0)
                {
                    doc.Add(Line());
                    doc.Add(Text("Este usuario no tiene registros!"));
                    doc.Add(Line());
                }

                doc.Close();

                writer.Flush();

                stream.Seek(0, SeekOrigin.Begin);

                string name = "compras";

                if (sale)
                {
                    name = "ventas";
                }

                string filename = $"reporte_{name}_{usuario.Nombre}_{DateTime.Now}.pdf";

                var response = HttpContext.Response;
                response.ContentType = "application/pdf";
                response.Headers.Add("Content-Disposition", $"attachment; filename={filename}");

                //return new EmptyResult();

                var file = new FileStreamResult(stream, "application/pdf");

                file.FileDownloadName = filename;

                return file;
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error al generar el reporte!");
            }
            return RedirectToAction("Index");
        }

        private Paragraph Title(string text)
        {
            Font font = new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD);
            var t = new Paragraph(text, font);
            t.Alignment = Element.ALIGN_CENTER;
            return t;
        }

        private Paragraph Text(string text)
        {
            Font font = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL);
            return new Paragraph(text, font);
        }

        private Paragraph Line()
        {
            return Text("______________________________________________________________________________");
        }

        private Paragraph Space()
        {
            return Text("\n");
        }

        private PdfPHeaderCell Th(string text)
        {
            Font font = new Font(Font.FontFamily.HELVETICA, 13, Font.BOLD);
            Paragraph p = new Paragraph(text, font);
            p.Alignment = Element.ALIGN_CENTER;
            PdfPHeaderCell cell = new PdfPHeaderCell();
            cell.AddElement(p);
            return cell;
        }

        private PdfPTable BuildProductTable(IEnumerable<Product> productos, bool stock = false)
        {
            int cols = 7;
            if (stock) cols = 6;
            var table = new PdfPTable(cols);
            table.WidthPercentage = 100;

            table.AddCell(Th("EAN"));
            table.AddCell(Th("Producto"));
            table.AddCell(Th("Categoria"));
            table.AddCell(Th("Descripcion"));
            if (stock)
            {
                table.AddCell(Th("Existencias"));
                table.AddCell(Th("Precio"));
            }
            else
            {
                table.AddCell(Th("Cantidad"));
                table.AddCell(Th("Precio"));
                table.AddCell(Th("Subtotal"));
            }


            foreach (var p in productos)
            {
                table.AddCell(p.EAN.ToString());
                table.AddCell(p.Name.ToString());
                table.AddCell(p.Category.ToString());
                table.AddCell(p.Description.ToString());
                if (stock)
                {
                    table.AddCell(p.Stock.ToString());
                    table.AddCell("$"+p.Price.ToString());
                }
                else
                {
                    table.AddCell(p.Cantidad.ToString());
                    table.AddCell("$" + p.Price.ToString());
                    table.AddCell("$" + (p.Price*p.Cantidad).ToString());
                }
            }
            return table;
        }
    }
}
