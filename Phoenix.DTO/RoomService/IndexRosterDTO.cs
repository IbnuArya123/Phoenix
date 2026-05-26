using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.DTO.RoomService {
    public class IndexRosterDTO {
        public string EmployeeNumber { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public TimeSpan? MondayRosterStart { get; set; }
        public TimeSpan? MondayRosterFinish { get; set; }
        public TimeSpan? TuesdayRosterStart { get; set; }
        public TimeSpan? TuesdayRosterFinish { get; set; }
        public TimeSpan? WednesdayRosterStart { get; set; }
        public TimeSpan? WednesdayRosterFinish { get; set; }
        public TimeSpan? ThursdayRosterStart { get; set; }
        public TimeSpan? ThursdayRosterFinish { get; set; }
        public TimeSpan? FridayRosterStart { get; set; }
        public TimeSpan? FridayRosterEnd { get; set; }
        public TimeSpan? SaturdayRosterStart { get; set; }
        public TimeSpan? SaturdayRosterFinish { get; set; }
        public TimeSpan? SundayRosterStart { get; set; }
        public TimeSpan? SundayRosterFinish { get; set; }
    }
}
