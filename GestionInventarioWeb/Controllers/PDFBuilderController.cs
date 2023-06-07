using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventarioWeb.Controllers
{
    public class PDFBuilderController : Controller
    {
        [Route("/pdf")]
        [Route("/pdf/{Name?}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult renderDocument(string? Name)
        {
            var doc = new Document();

            var writer = PdfWriter.GetInstance(doc, HttpContext.Response.Body);
            
            doc.Open();
            writer.Add(new Paragraph("Ke pasa"));
            doc.AddTitle("Titulo perron");
            doc.Add(new Paragraph("Hola mundo!"));
            doc.Add(new Paragraph("Hola "+Name));

            doc.Close();

            HttpContext.Response.ContentType = "application/pdf";

            HttpContext.Response.Headers.Add("content-disposition","attachment;filename=documento.pdf");

            return new EmptyResult();

        }
    }
}
