using Microsoft.AspNetCore.Mvc;

namespace Hello_Bechelor.Areas.Admin.Controllers
{
    public class BazarItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
