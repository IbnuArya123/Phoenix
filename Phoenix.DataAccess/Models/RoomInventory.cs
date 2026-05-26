using System;
using System.Collections.Generic;

namespace Phoenix.DataAccess.Models
{
    public partial class RoomInventory
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public string InventoryName { get; set; } = null!;
        public int Quantity { get; set; }

        public virtual Inventory InventoryNameNavigation { get; set; } = null!;
        public virtual Room RoomNumberNavigation { get; set; } = null!;
    }
}
