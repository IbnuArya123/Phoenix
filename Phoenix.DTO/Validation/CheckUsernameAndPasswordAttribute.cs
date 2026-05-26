using Phoenix.DataAccess.Models;
using Phoenix.DTO.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Validation {
    internal class CheckUsernameAndPasswordAttribute : ValidationAttribute {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            
            try {
                string password = value!.ToString()!;
                var dto = (LoginDTO)validationContext.ObjectInstance;
                string username = dto.Username;
                string role = dto.Role;
                if (role == "Administrator") {
                    bool isValid = IsAccountAdminAndPasswordCorrect(username, password);
                    if (isValid) {
                        return ValidationResult.Success;
                    }
                } else if (role == "Guest") { 
                    bool isValid = IsAccountGuestAndPasswordCorrect(username, password);
                    if (isValid) {
                        return ValidationResult.Success;
                    }
                }
                return new ValidationResult(ErrorMessage);
            } catch {
                return new ValidationResult("Invalid Data Type");
            }
        }

        private bool IsAccountAdminAndPasswordCorrect(string username, string password) {
            bool isValid = false;
            using(var dbContext = new PhoenixContext()) {
                var account = dbContext.Administrators.SingleOrDefault(c => c.Username == username);
                if (account != null) { 
                    isValid = BCrypt.Net.BCrypt.Verify(password, account.Password);
                }
            }
            return isValid;
        }

        private bool IsAccountGuestAndPasswordCorrect(string username, string password) {
            bool isValid = false;
            using(var dbContext = new PhoenixContext()) {
                var account = dbContext.Guests.SingleOrDefault(c => c.Username == username);
                if (account != null) { 
                    isValid = BCrypt.Net.BCrypt.Verify(password, account.Password);
                }
            }
            return isValid;
        }
    }
}
