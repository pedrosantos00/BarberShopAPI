using BarberShopAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.DAL
{
    public class AppointmentRepository
    {
        private readonly BarberShopDbContext _context;
        public AppointmentRepository(BarberShopDbContext context) => _context = context;
        //CRUD

        //CREATE
        public async Task<int> Create(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment.Id;
        }

        //RETRIEVE
        public async Task<Appointment?> GetById(int id)
        {
            Appointment? appointments = await _context.Appointments

                //.Include(r => r.)
                .FirstOrDefaultAsync(u => u.Id == id);


            return appointments;
        }

        public async Task<Appointment> GetByName(string title)
        {
            return await _context.Appointments.FirstOrDefaultAsync(u => u.Barber.Name == title);
        }

        public async Task<IEnumerable<Appointment>> GetByAppointmentId(int id, int startIndex, int itemCount)
        {
            IQueryable<Appointment> query = _context.Appointments

                .Where(u =>
                    u.Id == id
                ).Skip(startIndex).Take(itemCount);

            var appointments = await query.ToListAsync();



            return appointments;
        }

        //UPDATE
        public async Task<int> Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment.Id;
        }

        //DELETE
        public async void Delete(Appointment appointments)
        {
            _context.Appointments.Remove(appointments);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Appointment>> GetAppointments()
        {

            IEnumerable<Appointment> appointments = _context.Appointments
                .Include(b => b.Barber)
                .Include(b => b.Client);

            appointments.ToList();

            return appointments;
        }
    }
}
