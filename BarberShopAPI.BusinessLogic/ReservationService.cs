using BarberShopAPI.DAL;
using BarberShopAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.BusinessLogic
{
    public class ReservationService
    {
        private readonly ReservationRepository _reservationRepository;
        public ReservationService(ReservationRepository reservationRepository) => _reservationRepository = reservationRepository;

        //CRUD

        //CREATE
        public async Task<int> Create(Reservation reservation)
        {
            var id = 0;
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));

            if (reservation != null) id = await _reservationRepository.Create(reservation);

            return id;
        }
        //RETRIEVE
        public async Task<Reservation?> GetById(int id)
        {
            return await _reservationRepository.GetById(id);
        }

        public async Task<Reservation> GetByName(string name)
        {
            return await _reservationRepository.GetByName(name);
        }

        //public async Task<List<Reservation>> Search(string? filterword, int startIndex, int itemCount)
        //{
        //    IEnumerable<Reservation> reservationList = await _reservationRepository.Search(filterword, startIndex, itemCount);

        //    return reservationList.ToList();
        //}

        public async Task<List<Reservation>> GetByUserId(int id, int startIndex, int itemCount)
        {
            IEnumerable<Reservation> reservationList = await _reservationRepository.GetByBarberId(id, startIndex, itemCount);

            return reservationList.ToList();
        }

        //DELETE
        public async Task<bool> Delete(int id)
        {
            Reservation? reservationToDelete = await _reservationRepository.GetById(id);
            if (reservationToDelete != null)
            {
                _reservationRepository.Delete(reservationToDelete);
                return true;
            }
            else return false;
        }

        public async Task<int> UpdateRate(Reservation reservation)
        {
            return await _reservationRepository.Update(reservation);
        }

        public async Task<int> UpdateFav(Reservation reservation)
        {
            return await _reservationRepository.Update(reservation);
        }

        public async Task<int> UpdateComment(Reservation reservation)
        {
            return await _reservationRepository.Update(reservation);
        }
    }
}
