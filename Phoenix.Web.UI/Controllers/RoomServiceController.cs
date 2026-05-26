using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phoenix.DTO.Room;
using Phoenix.DTO.RoomService;
using Phoenix.Provider;
using System.Data;

namespace Phoenix.Web.UI.Controllers {

    [Authorize(Policy = "Administrator")]
    public class RoomServiceController : BaseController {

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult GetRoomService(string? empNumber, string? fullname, int page = 1) {
            try {
                empNumber = (empNumber == null) ? "" : empNumber;
                fullname = (fullname == null) ? "" : fullname;
                var provider = RoomServiceProvider.GetSingleton();
                var model = provider.GetRoomServiceTable(empNumber, fullname, page);
                model.CurrentPage = page;
                return Json(new { isException = false, data = model });
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult GetSingleEmployee(string empNumber) { 
            try {
                var provider = RoomServiceProvider.GetSingleton();
                var model = provider.GetSingleRoomService(empNumber);
                return Json(new { isException = false, data = model });
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] InsertRoomServiceDTO dto) { 
            try {
                if (ModelState.IsValid) {
                    var provider = RoomServiceProvider.GetSingleton();
                    var model = provider.InsertRoomService(dto);
                    return Json(new { isException = false, isValidation = false, data = model });
                }
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
            var errors = GetValidationErrorMessage(ModelState);
            return Json(new { isException = false, isValidation = true, validations = errors });
        }

        [HttpPost]
        public IActionResult Update([FromBody] UpdateRoomServiceDTO dto) { 
            try {
                if (ModelState.IsValid) {
                    var provider = RoomServiceProvider.GetSingleton();
                    provider.UpdateRoomService(dto);
                    return Json(new { isException = false, isValidation = false, data = dto });
                }
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
            var errors = GetValidationErrorMessage(ModelState);
            return Json(new { isException = false, isValidation = true, validations = errors });
        }

        [HttpGet]
        public IActionResult Roster(string empNumber) { 
            try { 
                var provider = RoomServiceProvider.GetSingleton();
                var model = provider.GetRosterIndex(empNumber);
                return View(model);
            } catch {
                return RedirectToAction(actionName: "InternalServer", controllerName: "Error");
            }
        }

        [HttpPost]
        public IActionResult SaveRoster([FromBody] UpsertRosterDTO dto) { 
            try {
                var provider = RoomServiceProvider.GetSingleton();
                provider.EditRoster(dto);
                return Json(new { isException = false, data = dto });
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
        }
    }
}
