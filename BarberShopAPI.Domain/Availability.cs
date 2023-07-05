﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.Domain
{
    public class Availability
    {
        public int Id { get; set; }
        public int BarberID { get; set; }
        public DateTime Date { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
