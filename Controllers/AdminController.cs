using Microsoft.AspNetCore.Mvc;

namespace RecycleCoinBlockExplorer.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
