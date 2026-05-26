using System;
using System.Collections.Generic;

namespace Phoenix.DataAccess.Models
{
    public partial class RoomService
    {
        public string EmployeeNumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? OutsourcingCompany { get; set; }
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
