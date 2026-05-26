using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phoenix.DTO.Room;
using Phoenix.Provider;

namespace Phoenix.Web.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RoomInventoryController : ControllerBase {

        [HttpGet]
        [HttpGet("room={roomNumber}")]
        [HttpGet("page={page}")]
        [HttpGet("room={roomNumber}/page={page}")]
        public IActionResult Get(string roomNumber, int page = 1) {
            try {
                var provider = RoomProvider.GetSingleton();
                var modelDTO = provider.GetRoomInventoryTable(roomNumber, page);
                return StatusCode(200, modelDTO);
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] UpsertRoomInventoryDTO dto) {
            try {
                var provider = RoomProvider.GetSingleton();
                var modelDTO = provider.InsertRoomInventory(dto);
                return StatusCode(200, modelDTO);
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id) { 
            try {
                var provider = RoomProvider.GetSingleton();
                provider.DeleteRoomInventory(id);
                return StatusCode(200, $"data dengan id: {id} berhasil di delete");
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }
    }
}
