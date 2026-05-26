using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phoenix.DTO.Room;
using Phoenix.Provider;
using System.Data;

namespace Phoenix.Web.UI.Controllers {

    [Authorize(Roles = "Administrator")]
    public class RoomController : BaseController {

        #region Room Index

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
        public IActionResult Insert() {
            var provider = RoomProvider.GetSingleton();
            var dto = new InsertRoomDTO();
            dto.RoomTypeDropdown = provider.GetRoomTypeDropdown();
            return View("Insert", dto);
        }

        [HttpPost]
        public IActionResult Insert(InsertRoomDTO dto) { 
            try {
                var provider = RoomProvider.GetSingleton();
                if (ModelState.IsValid) {
                    provider.InsertRoom(dto);
                    return RedirectToAction("Index");
                }
                dto.RoomTypeDropdown = provider.GetRoomTypeDropdown();
                return View("Insert", dto);
            } catch {
                return RedirectToAction(actionName: "InternalServer", controllerName: "Error");
            }
        }

        [HttpGet]
        public IActionResult Update(string roomNumber) { 
            try {
                var provider = RoomProvider.GetSingleton();
                var dto = provider.GetSingleRoom(roomNumber);
                dto.RoomTypeDropdown = provider.GetRoomTypeDropdown();
                return View("Update", dto);
            } catch {
                return RedirectToAction(actionName: "InternalServer", controllerName: "Error");
            }
        }

        [HttpPost]
        public IActionResult Update(UpdateRoomDTO dto) { 
            try {
                var provider = RoomProvider.GetSingleton();
                if (ModelState.IsValid) {
                    provider.UpdateRoom(dto);
                    return RedirectToAction("Index");
                }
                dto.RoomTypeDropdown = provider.GetRoomTypeDropdown();
                return View("Update", dto);
            } catch {
                return RedirectToAction(actionName: "InternalServer", controllerName: "Error");
            }
        }

        #endregion

        #region Room Details Index

        [HttpGet]
        public IActionResult Details(string roomNumber, int page = 1) {
            var provider = RoomProvider.GetSingleton();
            var model = provider.GetRoomInventoryTable(roomNumber, page);
            ViewBag.CurrentPage = page;
            return View(model);
        }

        [HttpPost]
        public IActionResult InsertRoomDetails([FromBody] UpsertRoomInventoryDTO dto) { 
            try {
                if (ModelState.IsValid) {
                    var provider = RoomProvider.GetSingleton();
                    var model = provider.InsertRoomInventory(dto);
                    provider.StockChange(dto);
                    return Json(new { isException = false, isValidation = false, data = model });
                }
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
            var errors = GetValidationErrorMessage(ModelState);
            return Json(new { isException = false, isValidation = true, validations = errors });
        }

        [HttpDelete]
        public IActionResult RemoveRoomDetail(long id) { 
            try {
                var provider = RoomProvider.GetSingleton();
                provider.DeleteRoomInventory(id);
                return Json(new { isException = false });
            } catch (Exception ex) {
                return Json(new { isException = true, message = $"Terjadi kesalahan dalam fetching data.\nError: {ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult GetInventoryDropdown() {
            var provider = RoomProvider.GetSingleton();
            var model = provider.GetInventoryDropdown();
            return Json(new { isException = false, data = model });
        }

        #endregion
    }
}
