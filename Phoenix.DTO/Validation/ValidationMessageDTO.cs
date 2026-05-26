using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Validation
{
    public class ValidationMessageDTO
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
