using Phoenix.DTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Reservation {
    public class InsertReservationDTO {
        public string Username { get; set; }
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public string RoomType { get; set; }
        public int GuestLimit { get; set; }
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Harap masukkan tanggal check in")]
        [CheckInDateChecker(ErrorMessage = "Tidak bisa check in sebelum hari ini")]
        public string CheckIn { get; set; }

        [Required(ErrorMessage = "Harap masukkan tanggal check out")]
        [CheckOutDateChecker(ErrorMessage = "Tanggal check out harus melewati tanggal check in")]
        public string CheckOut { get; set; }
        public decimal TotalCost { get; set; }
        public List<DropdownDTO>? PaymentMethodDropdown { get; set; }

        [Required(ErrorMessage = "Harap masukkan cara pembayaran")]
        public string PaymentMethod { get; set; }
        public string? Remark { get; set; }
    }
}
