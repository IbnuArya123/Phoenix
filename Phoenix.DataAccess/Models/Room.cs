using System;
using System.Collections.Generic;

namespace Phoenix.DataAccess.Models
{
    public partial class Room
    {
        public Room()
        {
            Reservations = new HashSet<Reservation>();
            RoomInventories = new HashSet<RoomInventory>();
        }

        public string Number { get; set; } = null!;
        public int Floor { get; set; }
        public string RoomType { get; set; } = null!;
        public int GuestLimit { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<RoomInventory> RoomInventories { get; set; }
    }
}
