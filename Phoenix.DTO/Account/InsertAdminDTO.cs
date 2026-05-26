using Phoenix.DTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Account {
    public class InsertAdminDTO {
        [Required(ErrorMessage = "Harap masukkan username")]
        [UniqueAdminUsername]
        public string Username { get; set; }

        [Required(ErrorMessage = "Harap masukkan Job Title")]
        public string JobTitle { get; set; }
    }
}
