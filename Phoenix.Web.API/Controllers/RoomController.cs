using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phoenix.DTO.Room;
using Phoenix.Provider;

namespace Phoenix.Web.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase {

        [HttpGet]
        [HttpGet("page={page}")]
        [HttpGet("room={roomNumber}")]
        [HttpGet("type={roomType}")]
        [HttpGet("status={status}")]
        [HttpGet("page={page}/room={roomNumber}/type={roomType}")]
        [HttpGet("page={page}/room={roomNumber}/type={roomType}/status={status}")]
        public IActionResult Get(int page = 1, string? roomNumber = "", string? roomType = "", string? status = "") {
            try {
                roomNumber = (roomNumber == null) ? "" : roomNumber;
                roomType = (roomType == null) ? "" : roomType;
                status = (status == null) ? "" : status;
                var provider = RoomProvider.GetSingleton();
                var modelDTO = provider.GetRoomTable(page, roomNumber, roomType, status);
                return StatusCode(200, modelDTO);
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] InsertRoomDTO dto) { 
            try {
                var provider = RoomProvider.GetSingleton();
                var modelDTO = provider.InsertRoom(dto);
                return StatusCode(200, modelDTO);
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateRoomDTO dto) {
            try {
                var provider = RoomProvider.GetSingleton();
                provider.UpdateRoom(dto);
                return StatusCode(200, dto);
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }
    }
}
