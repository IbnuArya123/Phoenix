using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.Reservation {
    public class IndexReservationDTO {
        public List<ReservationRowDTO> Table { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
