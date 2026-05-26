using Phoenix.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Validation {
    public class UniqueRoomNumberAttribute : ValidationAttribute {

        public override bool IsValid(object? value) {
            if (value == null)  return true;
            string room = value.ToString();
            return !FoundSameRoomNumber(room);
        }

        private bool FoundSameRoomNumber(string roomNumber) {
            bool found = false;
            using(var dbContext = new PhoenixContext()) { 
                found = dbContext.Rooms.Any(room => room.Number == roomNumber);
            }
            return found;
        }
    }
}
