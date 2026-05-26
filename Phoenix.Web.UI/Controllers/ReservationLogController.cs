using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phoenix.DTO.Reservation;
using Phoenix.Provider;

namespace Phoenix.Web.UI.Controllers {

    [Authorize(Roles = "Administrator")]
    public class ReservationLogController : BaseController {

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult GetReservationLog(string? roomNumber = "", int page = 1) {
            try {
                roomNumber = (roomNumber == null) ? "" : roomNumber;
                var provider = ReservationProvider.GetSingleton();
                var model = provider.GetReservationLog(roomNumber, page);
                model.CurrentPage = page;
                return Json(new { isException = false, data = model });
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult Detail(string code) { 
            try { 
                var provider = ReservationProvider.GetSingleton();
                var model = provider.GetSingleReservationDetail(code);
                return View(model);
            } catch {
                return RedirectToAction(actionName: "InternalServer", controllerName: "Error");
            }
        }

        [HttpGet]
        public IActionResult GetYearDropdown() {
            var provider = ReservationProvider.GetSingleton();
            var model = provider.GetYearDropdown();
            return Json(new { isException = false, data = model });
        }

        [HttpGet]
        public IActionResult GetMonthDropdown() {
            var provider = ReservationProvider.GetSingleton();
            var model = provider.GetMonthDropdown();
            return Json(new { isException = false, data = model });
        }

        [HttpPost]
        public IActionResult GetTotalIncome([FromBody] MonthAndYearIncomeDTO dto) { 
            try {
                    var provider = ReservationProvider.GetSingleton();
                    var model = provider.GetTotalIncome(dto);
                    return Json(new { isException = false, isValidation = false, data = model });
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
        }
    }
}
