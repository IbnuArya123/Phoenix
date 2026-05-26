using System;
using System.Collections.Generic;

namespace Phoenix.DataAccess.Models
{
    public partial class Inventory
    {
        public Inventory()
        {
            RoomInventories = new HashSet<RoomInventory>();
        }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Stock { get; set; }

        public virtual ICollection<RoomInventory> RoomInventories { get; set; }
    }
}
