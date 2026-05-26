using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phoenix.DTO.RoomService;
using Phoenix.Provider;

namespace Phoenix.Web.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RosterController : ControllerBase {

        [HttpGet]
        [HttpGet("empNumber={empNumber}")]
        public IActionResult Get(string empNumber) {
            try {
                var provider = RoomServiceProvider.GetSingleton();
                var modelDTO = provider.GetRosterIndex(empNumber);
                return StatusCode(200, modelDTO);
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] UpsertRosterDTO dto) { 
            try {
                var provider = RoomServiceProvider.GetSingleton();
                provider.EditRoster(dto);
                return StatusCode(200, dto);
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }
    }
}
