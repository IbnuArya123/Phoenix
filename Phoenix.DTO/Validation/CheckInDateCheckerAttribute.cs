using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Validation {
    internal class CheckInDateCheckerAttribute : ValidationAttribute {
        public override bool IsValid(object? value) {
            if (value == null) return true;
            string tanggalHTML = value.ToString()!;
            DateTime tanggal = DateTime.Parse(tanggalHTML);
            return tanggal >= DateTime.Now;
        }
    }
}
