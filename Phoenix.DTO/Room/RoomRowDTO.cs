using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Room {
    public class RoomRowDTO {
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public string Type { get; set; }
        public int GuestLimit { get; set; }
        public string Cost { get; set; }
        public string Status { get; set; }
    }
}
