using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Inventory {
    public class UpsertInventoryDTO {
        [Required(ErrorMessage = "Harap masukkan nama inventori")]
        public string Name { get; set; }

        [Range(1, 100, ErrorMessage = "HArap masukkan stock yg bnar (1 - 100)")]
        [Required(ErrorMessage = "Harap masukkan jumlah stock")]
        public int Stock { get; set; }
        public string? Description { get; set; }
    }
}
