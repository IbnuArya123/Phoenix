using Phoenix.DTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Room {
    public class UpsertRoomInventoryDTO {
        public long Id { get; set; }
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "Inventory Name harus ada")]
        [CheckExistingInventory(ErrorMessage = "Barang ini sudah ada di ruangan ini")]
        public string InventoryName { get; set; }

        [Range(1,200, ErrorMessage = "Harap masukkan quantity")]
        [CheckStock(ErrorMessage = "Tidak bisa melebihi stock")]
        public int Quantity { get; set; }
    }
}
