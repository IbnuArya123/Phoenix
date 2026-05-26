using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Room {
    public class IndexRoomDTO {
        public List<RoomRowDTO> Table { get; set; }
        public int TotalPages { get; set; }
        public List<DropdownDTO> RoomType { get; set; }
    }
}
