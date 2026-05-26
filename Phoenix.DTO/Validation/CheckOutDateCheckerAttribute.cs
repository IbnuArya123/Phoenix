using Phoenix.DTO.Reservation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Validation {
    internal class CheckOutDateCheckerAttribute : ValidationAttribute {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            try {
                string checkOut = value!.ToString()!;
                var dto = (InsertReservationDTO)validationContext.ObjectInstance;
                string checkIn = dto.CheckIn;
                DateTime tanggalIn = DateTime.Parse(checkIn);
                DateTime tanggalOut = DateTime.Parse(checkOut);
                bool isValid = DateChecker(tanggalIn, tanggalOut);

                if (!isValid) {
                    return new ValidationResult(ErrorMessage);
                }
            } catch {
                return new ValidationResult("Invalid Data Type");
            }
            return ValidationResult.Success;
        }

        private bool DateChecker(DateTime checkIn, DateTime checkOut) {
            return checkOut >= checkIn;
        }
    }
}
