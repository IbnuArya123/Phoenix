using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Room {
    public class RoomInventoryRowDTO {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; }
    }
}
