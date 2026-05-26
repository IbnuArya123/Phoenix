using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Phoenix.DTO.Inventory;
using Phoenix.Provider;
using Phoenix.Web.UI.Models;
using System.Diagnostics;

namespace Phoenix.Web.UI.Controllers {

    [Authorize(Roles = "Administrator")]
    public class InventoryController : BaseController {

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult GetInventory(int page = 1) {
            try {
                var provider = InventoryProvider.GetSingleton();
                var model = provider.GetInventoryTable(page);
                model.CurrentPage = page;
                return Json(new { isException = false, data = model });
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
        }

        public IActionResult GetSingleInventory(string name) { 
            try {
                var provider = InventoryProvider.GetSingleton();
                var model = provider.GetSingleInventory(name);
                return Json(new { isException = false, data = model });
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] UpsertInventoryDTO dto) { 
            try {
                if (ModelState.IsValid) {
                    var provider = InventoryProvider.GetSingleton();
                    var model = provider.InsertInventory(dto);
                    return Json(new { isException = false, data = model });
                }
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
            var errors = GetValidationErrorMessage(ModelState);
            return Json(new { isException = false, isValidation = true, validations = errors });
        }

        [HttpPost]
        public IActionResult Update([FromBody] UpsertInventoryDTO dto) {
            try {
                if (ModelState.IsValid) {
                    var provider = InventoryProvider.GetSingleton();
                    provider.UpdateInventory(dto);
                    return Json(new { isException = false, data = dto });
                }
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
            var errors = GetValidationErrorMessage(ModelState);
            return Json(new { isException = false, isValidation = true, validations = errors });
        }

        [HttpDelete]
        public IActionResult Delete(string name) { 
            try {
                var provider = InventoryProvider.GetSingleton();
                int model = provider.DeleteInventory(name);
                return Json(new { isException = false, data = model });
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
        }
    }
}