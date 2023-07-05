using BarberShopAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.DAL
{
    public class ReservationRepository
    {

        private readonly BarberShopDbContext _context;
        public ReservationRepository(BarberShopDbContext context) => _context = context;
        //CRUD

        //CREATE
        public async Task<int> Create(Reservation recipe)
        {
            await _context.Reservations.AddAsync(recipe);
            await _context.SaveChangesAsync();
            return recipe.Id;
        }

        //RETRIEVE
        public async Task<Reservation?> GetById(int id)
        {
            Reservation? recipe = await _context.Reservations
                
                //.Include(r => r.)
                .FirstOrDefaultAsync(u => u.Id == id);


            return recipe;
        }

        public async Task<Reservation> GetByName(string title)
        {
            return await _context.Reservations.FirstOrDefaultAsync(u => u.Client.Name == title);
        }

        public async Task<IEnumerable<Reservation>> GetByBarberId(int id, int startIndex, int itemCount)
        {
            IQueryable<Reservation> query = _context.Reservations
                
                .Where(u =>
                    u.BarberId == id
                ).Skip(startIndex).Take(itemCount);

            var recipes = await query.ToListAsync();



            return recipes;
        }

        //UPDATE
        public async Task<int> Update(Reservation recipe)
        {
            _context.Reservations.Update(recipe);
            await _context.SaveChangesAsync();
            return recipe.Id;
        }

        //DELETE
        public async void Delete(Reservation recipe)
        {
            _context.Reservations.Remove(recipe);
            _context.SaveChanges();
        }
    }
}
