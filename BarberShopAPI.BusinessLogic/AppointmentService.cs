using BarberShopAPI.DAL;
using BarberShopAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.BusinessLogic
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;
        public AppointmentService(AppointmentRepository appointmentRepository) => _appointmentRepository = appointmentRepository;

        //CRUD

        //CREATE
        public async Task<int> Create(Appointment appointment)
        {
            var id = 0;
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));

            if (appointment != null) id = await _appointmentRepository.Create(appointment);

            return id;
        }
        //RETRIEVE
        public async Task<Appointment?> GetById(int id)
        {
            return await _appointmentRepository.GetById(id);
        }


        //RETRIEVE All appointments
        public async Task<List<Appointment>> GetAppointments()
        {
            IEnumerable<Appointment> appointmentsResult = await _appointmentRepository.GetAppointments();

            return appointmentsResult.ToList();
        }



        //RETRIEVE
        public async Task<Appointment?> GetAvailability(int id)
        {
            return await _appointmentRepository.GetById(id);
        }


        public async Task<Appointment> GetByName(string name)
        {
            return await _appointmentRepository.GetByName(name);
        }

        //public async Task<List<Appointment>> Search(string? filterword, int startIndex, int itemCount)
        //{
        //    IEnumerable<Appointment> appointmentList = await _appointmentRepository.Search(filterword, startIndex, itemCount);

        //    return appointmentList.ToList();
        //}

        public async Task<List<Appointment>> GetByUserId(int id, int startIndex, int itemCount)
        {
            IEnumerable<Appointment> appointmentList = await _appointmentRepository.GetByAppointmentId(id, startIndex, itemCount);

            return appointmentList.ToList();
        }

        //DELETE
        public async Task<bool> Delete(int id)
        {
            Appointment? appointmentToDelete = await _appointmentRepository.GetById(id);
            if (appointmentToDelete != null)
            {
                _appointmentRepository.Delete(appointmentToDelete);
                return true;
            }
            else return false;
        }

        public async Task<int> UpdateRate(Appointment appointment)
        {
            return await _appointmentRepository.Update(appointment);
        }

        public async Task<int> UpdateFav(Appointment appointment)
        {
            return await _appointmentRepository.Update(appointment);
        }

        public async Task<int> UpdateComment(Appointment appointment)
        {
            return await _appointmentRepository.Update(appointment);
        }


    }
}
