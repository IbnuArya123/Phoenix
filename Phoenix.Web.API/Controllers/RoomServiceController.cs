using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phoenix.DTO.RoomService;
using Phoenix.Provider;

namespace Phoenix.Web.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RoomServiceController : ControllerBase {

        [HttpGet]
        [HttpGet("page={page}")]
        [HttpGet("employeeNumber={empNumber}")]
        [HttpGet("fullName={fullname}")]
        [HttpGet("page={page}/employeeNumber={empNumber}/fullName={fullname}")]
        public IActionResult Get(string? empNumber, string? fullname, int page = 1) {
            try {
                empNumber = (empNumber == null) ? "" : empNumber;
                fullname = (fullname == null) ? "" : fullname;
                var provider = RoomServiceProvider.GetSingleton();
                var modelDTO = provider.GetRoomServiceTable(empNumber, fullname, page);
                return StatusCode(200, modelDTO);
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] InsertRoomServiceDTO dto) { 
            try {
                if (ModelState.IsValid) {
                    var provider = RoomServiceProvider.GetSingleton();
                    var modelDto = provider.InsertRoomService(dto);
                    return StatusCode(200, modelDto);
                }
                return StatusCode(400, "Masih ada input yang tidak valid");
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }
    }
}
