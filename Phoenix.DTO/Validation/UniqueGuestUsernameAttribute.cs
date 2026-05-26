using Phoenix.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Validation {
    internal class UniqueGuestUsernameAttribute : ValidationAttribute {
        public override bool IsValid(object? value) {
            if (value == null)  return true;
            string username = value.ToString();
            return !FoundSameAdminUsername(username);
        }

        private bool FoundSameAdminUsername(string username) {
            bool found = false;
            using(var dbContext = new PhoenixContext()) { 
                found = dbContext.Guests.Any(room => room.Username == username);
            }
            return found;
        }
    }
}
