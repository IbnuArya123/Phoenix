using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Provider;
using System.Data;

namespace Phoenix.Web.UI.Controllers {

    [Authorize(Roles = "Guest")]
    public class MyRoomController : Controller {
        public IActionResult Index(string username) {
            var provider = ReservationProvider.GetSingleton();
            var model = provider.GetGuestBookedRoom(username);
            return View(model);
        }
    }
}
