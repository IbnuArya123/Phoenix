using Phoenix.DataAccess.Models;
using Phoenix.DTO.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Validation {
    internal class CheckOldPasswordAttribute : ValidationAttribute {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {

            try {
                string oldPassword = value!.ToString()!;
                var dto = (ChangeAccountPasswordDTO)validationContext.ObjectInstance;
                string username = dto.Username;
                string role = dto.Role;
                bool isSame = IsOldPasswordSame(oldPassword, username, role);

                if (!isSame) {
                    return new ValidationResult(ErrorMessage);
                }
            } catch {
                return new ValidationResult("Invalid Data Type");
            }
            return ValidationResult.Success;
        }

        private bool IsOldPasswordSame(string password, string username, string role) {
            bool isSame = true;

            if (role == "Administrator") {
                using (var dbContext = new PhoenixContext()) {
                    var selectedEntity = dbContext.Administrators.SingleOrDefault(acc => acc.Username == username);
                    isSame = BCrypt.Net.BCrypt.Verify(password, selectedEntity.Password);
                }
            } else if (role == "Guest") {
                using (var dbContext = new PhoenixContext()) {
                    var selectedEntity = dbContext.Guests.SingleOrDefault(acc => acc.Username == username);
                    isSame = BCrypt.Net.BCrypt.Verify(password, selectedEntity.Password);
                }
            }

            return isSame;
        }
    }
}
