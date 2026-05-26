using Phoenix.DTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Account {
    public class ChangeAccountPasswordDTO {
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Harap masukkan password lama anda")]
        [CheckOldPassword(ErrorMessage = "Harap masukkan password lama dengan benar")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Harap masukkan password baru anda")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Harap masukkan konfirmasi password anda")]
        [Compare("NewPassword", ErrorMessage = "Password tidak sama")]
        public string NewPasswordConfirmation { get; set; }

        public string Role { get; set; }
    }
}
