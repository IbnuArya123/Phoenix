using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Reservation {
    public class GuestRoomDetailRowDTO {
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public string RoomType { get; set; }
        public int GuestLimit { get; set; }
        public string Cost { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string TotalCost { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Remark { get; set; }
        public string BookingStatus { get; set; }
    }
}
