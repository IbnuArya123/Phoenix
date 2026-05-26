using Phoenix.DTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.RoomService {
    public class UpdateRoomServiceDTO {
        [Required(ErrorMessage = "Employee Number harus ada")]
        public string EmployeeNumber { get; set; }

        [Required(ErrorMessage = "Nama depan harus ada")]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Company harus ada")]
        public string Company { get; set; }
    }
}
