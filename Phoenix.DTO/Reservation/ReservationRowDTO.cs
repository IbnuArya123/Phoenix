using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Reservation {
    public class ReservationRowDTO {
        public string Code { get; set; }
        public string RoomNumber { get; set; }
        public string Username { get; set; }
        public string BookDate { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string PaymentDate { get; set; }
    }
}
