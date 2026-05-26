using Phoenix.DataAccess.Models;
using Phoenix.DTO.Inventory;
using Phoenix.DTO.Room;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Validation {
    internal class CheckExistingInventoryAttribute : ValidationAttribute {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            try {
                string inventoryName = value.ToString()!;
                var dto = (UpsertRoomInventoryDTO)validationContext.ObjectInstance;
                string room = dto.RoomNumber;
                int isValid = IsInventoryExist(room, inventoryName);

                if (isValid > 0) {
                    return new ValidationResult(ErrorMessage);
                }
            } catch {
                return new ValidationResult("Invalid Data Type");
            }
            return ValidationResult.Success;
        }

        private int IsInventoryExist(string roomNumber, string inventoryName) {
            int count = 0;

            using(var dbContext = new PhoenixContext()) { 
                count = dbContext.RoomInventories.Where(r => r.RoomNumber == roomNumber).Where(r => r.InventoryName == inventoryName).Count();
                Console.WriteLine(count);
            }

            return count;
        }
    }
}
