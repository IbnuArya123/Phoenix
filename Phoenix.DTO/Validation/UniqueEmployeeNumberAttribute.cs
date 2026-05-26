using Phoenix.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Validation {
    public class UniqueEmployeeNumberAttribute : ValidationAttribute {

        public override bool IsValid(object? value) {
            if (value == null)  return true;
            string employeeNumber = value.ToString();
            return !FoundSameEmployeeNumber(employeeNumber);
        }

        private bool FoundSameEmployeeNumber(string employeeNumber) {
            bool found = false;
            using(var dbContext = new PhoenixContext()) { 
                found = dbContext.RoomServices.Any(serv => serv.EmployeeNumber == employeeNumber);
            }
            return found;
        }
    }
}
