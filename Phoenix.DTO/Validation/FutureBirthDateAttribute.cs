using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Validation {
    internal class FutureBirthDateAttribute : ValidationAttribute {
        public override bool IsValid(object? value) {
            if (value == null) return true;
            DateTime birthDate = DateTime.Parse(value.ToString()!);
            return birthDate < DateTime.Now;
        }
    }
}
