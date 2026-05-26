using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phoenix.DTO.Reservation;
using Phoenix.Provider;

namespace Phoenix.Web.UI.Controllers {

    [Authorize(Roles = "Guest")]
    public class BookingController : Controller {

        [HttpGet]
        public IActionResult Index(int page = 1, string? roomNumber = "", string? roomType = "", string? status = "") {
            try {
                roomNumber = (roomNumber == null) ? "" : roomNumber;
                roomType = (roomType == null) ? "" : roomType;
                status = (status == null) ? "" : status;
                var provider = RoomProvider.GetSingleton();
                var model = provider.GetRoomTable(page, roomNumber, roomType, status);
                model.RoomType = provider.GetRoomTypeDropdown().ToList();
                ViewBag.RoomNumber = roomNumber;
                ViewBag.RoomType = roomType;
                ViewBag.Status = status;
                ViewBag.CurrentPage = page;
                return View(model);
            } catch {
                return RedirectToAction(actionName: "InternalServer", controllerName: "Error");
            }
        }

        [HttpGet]
        public IActionResult Detail(string roomNumber, string status) {
            var provider = RoomProvider.GetSingleton();
            var model = provider.GetRoomDetail(roomNumber, status);
            return View(model);
        }

        [HttpGet]
        public IActionResult Reservation(string roomNumber, string username) { 
            var provider = ReservationProvider.GetSingleton();
            var dto = provider.GetSingleRoomReservation(roomNumber, username);
            dto.PaymentMethodDropdown = provider.GetPaymentMethodDropdown();
            return View(dto);
        }

        [HttpPost]
        public IActionResult Reservation(InsertReservationDTO dto) {
            try {
                var provider = ReservationProvider.GetSingleton();
                if (ModelState.IsValid) {
                    provider.InsertNewReservation(dto);
                    return RedirectToAction("Index");
                }
                dto.PaymentMethodDropdown = provider.GetPaymentMethodDropdown();
                return View("Reservation", dto);
            } catch {
                return RedirectToAction(actionName: "InternalServer", controllerName: "Error");
            }
        }
    }
}
