﻿using BarberShopAPI.DAL;
using BarberShopAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.BusinessLogic
{
    public class BarberService
    {
        private readonly BarberRepository _barberRepository;
        public BarberService(BarberRepository barberRepository) => _barberRepository = barberRepository;

        //CRUD

        //CREATE
        public async Task<int> Create(Barber barber)
        {
            var id = 0;
            if (barber == null) throw new ArgumentNullException(nameof(barber));

            if (barber != null) id = await _barberRepository.Create(barber);

            return id;
        }
        //RETRIEVE
        public async Task<Barber?> GetById(int id)
        {
            return await _barberRepository.GetById(id);
        }


        //RETRIEVE All barbers
        public async Task<List<Barber>> GetBarbers()
        {
            IEnumerable<Barber> barbersResult = await _barberRepository.GetBarbers();

            return barbersResult.ToList();
        }



        //RETRIEVE
        public async Task<Barber?> GetAvailability(int id)
        {
            return await _barberRepository.GetById(id);
        }


        public async Task<Barber> GetByName(string name)
        {
            return await _barberRepository.GetByName(name);
        }

        //public async Task<List<Barber>> Search(string? filterword, int startIndex, int itemCount)
        //{
        //    IEnumerable<Barber> barberList = await _barberRepository.Search(filterword, startIndex, itemCount);

        //    return barberList.ToList();
        //}

        public async Task<List<Barber>> GetByUserId(int id, int startIndex, int itemCount)
        {
            IEnumerable<Barber> barberList = await _barberRepository.GetByBarberId(id, startIndex, itemCount);

            return barberList.ToList();
        }

        //DELETE
        public async Task<bool> Delete(int id)
        {
            Barber? barberToDelete = await _barberRepository.GetById(id);
            if (barberToDelete != null)
            {
                _barberRepository.Delete(barberToDelete);
                return true;
            }
            else return false;
        }

        public async Task<int> UpdateRate(Barber barber)
        {
            return await _barberRepository.Update(barber);
        }

        public async Task<int> UpdateFav(Barber barber)
        {
            return await _barberRepository.Update(barber);
        }

        public async Task<int> UpdateComment(Barber barber)
        {
            return await _barberRepository.Update(barber);
        }

        public async Task<List<AvailabilityTimeSlot>> GetBarbersAvailability(DateTime desiredDate)
        {
            IEnumerable<Barber> barbers = await _barberRepository.GetBarbers();
            List<AvailabilityTimeSlot> availabilitySlots = new List<AvailabilityTimeSlot>();

            // Define the start and end time for appointments
            DateTime startTime = desiredDate.Date.AddHours(9);
            DateTime endTime = desiredDate.Date.AddHours(18);

            foreach (Barber barber in barbers)
            {
                // Get the existing appointments for the barber on the desired date
                List<Appointment> existingAppointments = barber.Appointments
                    .Where(a => a.AppointmentDate.Date == desiredDate.Date)
                    .ToList();

                // Create a list of all possible time slots within the specified time range
                List<DateTime> availableTimeSlots = new List<DateTime>();
                DateTime currentSlot = startTime;
                while (currentSlot < endTime)
                {
                    availableTimeSlots.Add(currentSlot);
                    currentSlot = currentSlot.AddMinutes(30); // Assuming each appointment is 30 minutes long
                }

                // Remove the time slots that are already booked
                foreach (var appointment in existingAppointments)
                {
                    availableTimeSlots.RemoveAll(slot => slot == appointment.AppointmentDate);
                }

                // Create an AvailabilityTimeSlot object for the barber
                AvailabilityTimeSlot availabilitySlot = new AvailabilityTimeSlot
                {
                    BarberId = barber.Id,
                    BarberName = barber.Name,
                    AvailableTimeSlots = availableTimeSlots
                };

                availabilitySlots.Add(availabilitySlot);
            }

            return availabilitySlots;
        }

    }
}