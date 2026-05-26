using Phoenix.DTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Account {
    public class LoginDTO {
        [Required (ErrorMessage = "Harap masukkan username")]
        public string Username { get; set; }

        [Required (ErrorMessage = "Harap masukkan password")]
        [CheckUsernameAndPassword(ErrorMessage = "Password atau Username salah")]
        public string Password { get; set; }

        [CheckAccountRole(ErrorMessage = "Harap masukkan role yang benar")]
        public string Role { get; set; }
    }
}
