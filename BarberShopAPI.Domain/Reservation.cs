﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.Domain
{
    public class Reservation
    {
        public int Id{ get; set; }
        public int BarberId { get; set; }
        public Client Client { get; set; }
        public string ServiceCategory { get; set; }
        public string Service { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Status { get; set; }
    }
}
