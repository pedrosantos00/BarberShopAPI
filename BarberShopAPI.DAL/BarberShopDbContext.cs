using BarberShopAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.DAL
{
    public class BarberShopDbContext : DbContext
    {
        public BarberShopDbContext(DbContextOptions<BarberShopDbContext> options) : base(options) { }


        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barber>()
                .HasMany(r => r.Appointments)
                .WithOne(u => u.Barber)
                .HasForeignKey(r => r.BarberId);

            modelBuilder.Entity<Client>()
                .HasMany(r => r.Appointments)
                .WithOne(u => u.Client)
                .HasForeignKey(r => r.ClientId);

        }


    }

    


}
