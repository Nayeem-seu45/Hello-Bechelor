using Hello_Bechelor.ServicesProvider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello_Bechelor.Areas.Admin.Controllers
{
    public class RentCostController : BaseController<RentCostController>
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
