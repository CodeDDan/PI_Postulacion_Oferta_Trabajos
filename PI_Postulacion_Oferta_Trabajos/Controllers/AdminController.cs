using Microsoft.AspNetCore.Mvc;

namespace PI_Postulacion_Oferta_Trabajos.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
