using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventarioWeb.Controllers
{
    public class PDFBuilderController : Controller
    {
        [HttpGet("/pdf/build", Name = "BuildPdf")]
        [AllowAnonymous]
        public IActionResult renderDocument(string? Text)
        {
            var stream = new MemoryStream();

            var doc = new Document(PageSize.LETTER);

            var writer = PdfWriter.GetInstance(doc, stream);

            writer.CloseStream = false;

            doc.Open();
            doc.AddTitle("Titulo perron");
            if(Text != null)
            {
                doc.Add(new Paragraph("Hola " + Text));
            }
            else
            {
                doc.Add(new Paragraph("Contenido vacio! "));
            }
            doc.Close();

            writer.Flush();

            stream.Seek(0, SeekOrigin.Begin);

            HttpContext.Response.ContentType = "application/pdf";

            HttpContext.Response.Headers.Add("content-disposition","attachment;filename=documento.pdf");

            //return new EmptyResult();

            var file = new FileStreamResult(stream, "application/pdf");

            file.FileDownloadName = $"report_{DateTime.Now}.pdf";

            return file;

        }

        [HttpGet("/Reports", Name = "Reports")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("Views/pdfs.cshtml");
        }
    }
}
