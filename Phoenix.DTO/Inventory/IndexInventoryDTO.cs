using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Inventory {
    public class IndexInventoryDTO {
        public List<InventoryRowDTO> Table { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
