using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.Domain
{
    public class AvailabilityTimeSlot
    {
        public int BarberId { get; set; }
        public string BarberName { get; set; }
        public List<DateTime> AvailableTimeSlots { get; set; }
    }
}
