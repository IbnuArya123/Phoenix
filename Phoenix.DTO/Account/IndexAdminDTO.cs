using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Account {
    public class IndexAdminDTO {
        public List<AdminRowDTO> Table { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
