using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phoenix.DTO.Inventory;
using Phoenix.Provider;

namespace Phoenix.Web.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase {

        [HttpGet("page={page}")]
        public IActionResult GetInventory(int page = 1) {
            try {
                var provider = InventoryProvider.GetSingleton();
                var modelDTO = provider.GetInventoryTable(page);
                return StatusCode(200, modelDTO);
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetInventoryName() {
            try {
                var provider = InventoryProvider.GetSingleton();
                var modelDTO = provider.GetName();
                return StatusCode(200, modelDTO);
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }

        [HttpGet("name={name}")]
        public IActionResult GetSingleInventory(string name) { 
            try {
                var provider = InventoryProvider.GetSingleton();
                var modelDTO = provider.GetSingleInventory(name);
                return StatusCode(200, modelDTO);
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] UpsertInventoryDTO dto) { 
            try {
                if (ModelState.IsValid) {
                    var provider = InventoryProvider.GetSingleton();
                    var modelDto = provider.InsertInventory(dto);
                    return StatusCode(200, modelDto);
                }
                return StatusCode(400, "Masih ada input yang tidak valid");
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpsertInventoryDTO dto) { 
            try {
                if (ModelState.IsValid) {
                    var provider = InventoryProvider.GetSingleton();
                    provider.UpdateInventory(dto);
                    return StatusCode(200, dto);
                }
                return StatusCode(400, "Masih ada input yang tidak valid");
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }

        [HttpDelete("name={name}")]
        public IActionResult Delete(string name) { 
            try {
                var provider = InventoryProvider.GetSingleton();
                int totalFK = provider.DeleteInventory(name);
                if(totalFK > 0) { 
                    return StatusCode(422, $"Ada sejumlah {totalFK} yang terhubung pada data ini" );
                }
                return Ok(name);
            } catch (Exception e) {
                return StatusCode(500, $"Ada kesalahan diserver, harap hubungi tim IT\nError: {e.Message}");
            }
        }
    }
}
