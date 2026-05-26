using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Room {
    public class IndexRoomInventoryDTO {
        public List<RoomInventoryRowDTO> Table { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public string RoomNumber { get; set; }
        public int RoomFloor { get; set; }
        public int GuestLimit { get; set; }
        public string RoomType { get; set; }
    }
}
