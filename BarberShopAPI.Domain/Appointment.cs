using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.Domain
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int BarberId { get; set; }
        public int ClientId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Barber Barber { get; set; }
        public Client Client { get; set; }
    }
}
