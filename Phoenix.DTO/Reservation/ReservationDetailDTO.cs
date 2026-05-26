using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Reservation {
    public class ReservationDetailDTO {
        public string ReservationCode { get; set; }
        public string ReservationMethod { get; set; }
        public string RoomNumber { get; set; }
        public int RoomFloor { get; set; }
        public string RoomType { get; set; }
        public string GuestUsername { get; set; }
        public string GuestFullName { get; set; }
        public string BookDate { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Cost { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Remark { get; set; }
    }
}
