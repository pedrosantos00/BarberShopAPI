using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.Domain
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Service{ get; set; }
        public float Cost{ get; set; }
        public int ExpectedTime{ get; set; }
        public string? Status { get; set; } = "Pending";
        public DateTime AppointmentDate { get; set; }

        public int BarberId { get; set; }
        public Barber? Barber { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
