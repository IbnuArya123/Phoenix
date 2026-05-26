using Phoenix.DTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Account {
    public class UpsertGuestDTO {
        [Required(ErrorMessage = "Harap masukkan username")]
        [UniqueGuestUsername(ErrorMessage = "Username sudah ada")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Harap masukkan password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Harap masukkan konfirmasi password")]
        [Compare("Password", ErrorMessage = "Password tidak sama")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Harap masukkan nama depan")]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Harap masukkan tanggal lahir")]
        [FutureBirthDate(ErrorMessage = "Tidak bisa memasukkan tanggal lahir masa depan")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "Harap masukkan jenis kelamin anda")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Harap masukkan warga negara anda")]
        public string Citizenship { get; set; }

        [Required(ErrorMessage = "Harap masukkan nomor KTP/Pasport")]
        public string IDNumber { get; set; }
    }
}
