using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phoenix.DTO.Account;
using Phoenix.Provider;

namespace Phoenix.Web.UI.Controllers {
    [Authorize(Roles = "Administrator")]
    public class AdminController : BaseController {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAdmin(int page = 1) {
            try {
                var provider = AccountProvider.GetSingleton();
                var model = provider.GetAdminTable(page);
                model.CurrentPage = page;
                return Json(new { isException = false, data = model });
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult GetSingleAdmin(string username) { 
            try {
                var provider = AccountProvider.GetSingleton();
                var model = provider.GetSingleAdmin(username);
                return Json(new { isException = false, data = model });
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] InsertAdminDTO dto) { 
            try {
                if (ModelState.IsValid) {
                    var provider = AccountProvider.GetSingleton();
                    var model = provider.RegisterAdmin(dto);
                    return Json(new { isException = false, data = model });
                }
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
            var errors = GetValidationErrorMessage(ModelState);
            return Json(new { isException = false, isValidation = true, validations = errors });
        }

        [HttpPost]
        public IActionResult Update([FromBody] UpdateAdminDTO dto) { 
            try {
                if (ModelState.IsValid) {
                    var provider = AccountProvider.GetSingleton();
                    provider.UpdateAdmin(dto);
                    return Json(new { isException = false, data = dto });
                }
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
            var errors = GetValidationErrorMessage(ModelState);
            return Json(new { isException = false, isValidation = true, validations = errors });
        }
    }
}
