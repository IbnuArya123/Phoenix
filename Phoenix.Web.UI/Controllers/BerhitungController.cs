using Microsoft.AspNetCore.Mvc;
using Phoenix.DTO;

namespace Phoenix.Web.UI.Controllers {
    public class BerhitungController : Controller {
        [HttpGet]
        public IActionResult Index()
        {
            var dto = new BerhitungDTO();
            return View(dto);
        }

        [HttpPost]
        public IActionResult Index(BerhitungDTO dto) {
            return View(dto);
        }

        [HttpGet]
        public IActionResult Index2(BerhitungDTO dto) {
            return View(dto);
        }

        [HttpPost]
        public IActionResult Index2Post(BerhitungDTO dto) {
            return View(dto);
        }

        [HttpGet]
        public IActionResult Index3(BerhitungDTO dto) {
            return View(dto);
        }

        [HttpPost]
        public IActionResult Index3Post(BerhitungDTO dto) {
            return View(dto);
        }
    }
}
