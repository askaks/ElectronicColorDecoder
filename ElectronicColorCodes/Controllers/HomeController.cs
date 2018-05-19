using System.Web.Mvc;

namespace ElectronicColorCodes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the Electronic Components Color Code Ohms Calculator ";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Electronic Components Color Code Ohms Calculator";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Joanna Szymczyk";

            return View();
        }
    }
}