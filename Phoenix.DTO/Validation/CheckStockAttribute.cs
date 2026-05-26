using Phoenix.DataAccess.Models;
using Phoenix.DTO.Room;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Validation {
    internal class CheckStockAttribute : ValidationAttribute {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            try {
                int quantity = (int)value!;
                var dto = (UpsertRoomInventoryDTO)validationContext.ObjectInstance;
                string name = dto.InventoryName;
                bool isValid = IsQuantityAboveStock(quantity, name);

                if (isValid == false) {
                    return new ValidationResult(ErrorMessage);
                }
            } catch {
                return new ValidationResult("Invalid Data Type");
            }
            return ValidationResult.Success;
        }

        private bool IsQuantityAboveStock(int quantity, string inventoryName) {
            bool isValid = true;

            using(var dbContext = new PhoenixContext()) { 
                var selectedEntity = dbContext.Inventories.SingleOrDefault(inv => inv.Name == inventoryName);
                return quantity <= selectedEntity.Stock;
            }
        }
    }
}
