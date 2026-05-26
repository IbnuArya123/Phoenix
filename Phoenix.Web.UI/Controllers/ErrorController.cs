using Microsoft.AspNetCore.Mvc;

namespace Phoenix.Web.UI.Controllers {
    public class ErrorController : Controller {
        public IActionResult InternalServer() {
            return View();
        }
    }
}
