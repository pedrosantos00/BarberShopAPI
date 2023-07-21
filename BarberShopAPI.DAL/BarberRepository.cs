using BarberShopAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.DAL
{
    public class BarberRepository
    {
        private readonly BarberShopDbContext _context;
        public BarberRepository(BarberShopDbContext context) => _context = context;
        //CRUD

        //CREATE
        public async Task<int> Create(Barber recipe)
        {
            await _context.Barbers.AddAsync(recipe);
            await _context.SaveChangesAsync();
            return recipe.Id;
        }

        //RETRIEVE
        public async Task<Barber?> GetById(int id)
        {
            Barber? recipe = await _context.Barbers

                //.Include(r => r.)
                .FirstOrDefaultAsync(u => u.Id == id);


            return recipe;
        }

        public async Task<Barber> GetByName(string title)
        {
            return await _context.Barbers.FirstOrDefaultAsync(u => u.Name == title);
        }

        public async Task<IEnumerable<Barber>> GetByBarberId(int id, int startIndex, int itemCount)
        {
            IQueryable<Barber> query = _context.Barbers

                .Where(u =>
                    u.Id == id
                ).Skip(startIndex).Take(itemCount);

            var recipes = await query.ToListAsync();



            return recipes;
        }

        //UPDATE
        public async Task<int> Update(Barber recipe)
        {
            _context.Barbers.Update(recipe);
            await _context.SaveChangesAsync();
            return recipe.Id;
        }

        //DELETE
        public async void Delete(Barber recipe)
        {
            _context.Barbers.Remove(recipe);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Barber>> GetBarbers()
        {

            IEnumerable<Barber> barbers = _context.Barbers
                .Include(b => b.Appointments);

            barbers.ToList();

            return barbers;
        }

        public bool RefreshTokenExists(string refreshtoken)
        {
            bool flag = _context.Barbers.Any(u => u.RefreshToken == refreshtoken);
            if (flag) return true; else return false;
        }
    }
}
