using Phoenix.DataAccess.Models;
using Phoenix.DTO.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Validation {
    internal class CheckAccountRoleAttribute : ValidationAttribute {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {

            try {
                string role = value!.ToString()!;
                var dto = (LoginDTO)validationContext.ObjectInstance;
                string username = dto.Username;
                if (role == "Administrator") {
                    bool isValid = IsAccountAdmin(username);
                    if (isValid) {
                        return ValidationResult.Success;
                    }
                } else if (role == "Guest") { 
                    bool isValid = IsAccountGuest(username);
                    if (isValid) {
                        return ValidationResult.Success;
                    }
                }
                return new ValidationResult(ErrorMessage);
            } catch {
                return new ValidationResult("Invalid Data Type");
            }
        }

        private bool IsAccountAdmin(string username) {
            bool isAdmin = false;
            using(var dbContext = new PhoenixContext()) { 
                var account = dbContext.Administrators.SingleOrDefault(c => c.Username == username);
                if (account != null) isAdmin = true;
            }

            return isAdmin;
        }

        private bool IsAccountGuest(string username) {
            bool isGuest = false;
            using(var dbContext = new PhoenixContext()) { 
                var account = dbContext.Guests.SingleOrDefault(c => c.Username == username);
                if (account != null) isGuest = true;
            }

            return isGuest;
        }
    }
}
