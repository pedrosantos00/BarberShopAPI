using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.Domain
{
    public class Barber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Availability> Availability { get; set; }
    }
}
