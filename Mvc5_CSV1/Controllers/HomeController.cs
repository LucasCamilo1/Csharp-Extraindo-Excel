using LumenWorks.Framework.IO.Csv;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Mvc5_CSV1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }
                        return View(csvTable);
                    }
                    else
                    {
                        ModelState.AddModelError("Arquivo", "Formato do arquivo não é suportado");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("Arquivo", "Faça o Upload do arquivo");
                }
            }
            return View();
        }
    }
}