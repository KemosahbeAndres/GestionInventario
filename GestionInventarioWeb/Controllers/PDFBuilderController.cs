using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventarioWeb.Controllers
{
    public class PDFBuilderController : Controller
    {
        [Route("/pdf/result/{Text?}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult renderDocument(string? Text)
        {
            var stream = new MemoryStream();
            var doc = new Document();
            var writer = PdfWriter.GetInstance(doc, stream);
            writer.CloseStream = false;

            doc.Open();
            doc.AddTitle("Titulo perron");
            doc.Add(new Paragraph(Text));
            doc.Close();

            stream.Seek(0, SeekOrigin.Begin);


            HttpContext.Response.ContentType = "application/pdf";

            HttpContext.Response.Headers.Add("content-disposition","attachment;filename=documento.pdf");

            return new FileStreamResult(stream, "application/pdf");

        }

        [Route("/pdf")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Home()
        {
            return View("Views/pdf.cshtml");
        }
    }
}
