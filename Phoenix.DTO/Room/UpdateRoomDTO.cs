using Phoenix.DTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Room {
    public class UpdateRoomDTO {
        [Required(ErrorMessage = "Harap masukkan nomor kmar")]
        [Display(Name = "Number*")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "Harap masukkan lantai kamar")]
        [Display(Name = "Floor*")]
        [Range(1, 15, ErrorMessage = "Harap pilih lantai dari 1 - 15")]
        public int Floor { get; set; }
        public List<DropdownDTO>? RoomTypeDropdown { get; set; }

        [Required(ErrorMessage = "Harap masukkan type kamar")]
        [StringLength(3, ErrorMessage = "Panjang hanya 3 char.")]
        [Display(Name = "Room Type*")]
        public string RoomType { get; set; }

        [Required(ErrorMessage = "Limit tamu harus di masukkan")]
        [Range(1, 10, ErrorMessage = "Limit tamu dari 1 - 10")]
        [Display(Name = "Guest Limit*")]
        public int GuestLimit { get; set; }

        [Required(ErrorMessage = "Harap masukkan harga kamar")]
        [Range(300000, int.MaxValue, ErrorMessage = "Di luar limit harga")]
        [Display(Name = "Cost*")]
        public decimal Cost { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}
