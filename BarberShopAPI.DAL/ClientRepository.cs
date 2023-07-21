using BarberShopAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.DAL
{
    public class ClientRepository
    {
        private readonly BarberShopDbContext _context;
        public ClientRepository(BarberShopDbContext context) => _context = context;
        //CRUD

        //CREATE
        public async Task<int> Create(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return client.Id;
        }

        //RETRIEVE
        public async Task<Client?> GetById(int id)
        {
            Client? client = await _context.Clients

                //.Include(r => r.)
                .FirstOrDefaultAsync(u => u.Id == id);


            return client;
        }

        public async Task<Client?> GetByPhoneNumber(string phoneNumber)
        {
            Client? client = await _context.Clients

                 //.Include(r => r.)
                 .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);


            return client;
        }

        public async Task<Client> GetByName(string title)
        {
            return await _context.Clients.FirstOrDefaultAsync(u => u.Name == title);
        }

        public async Task<IEnumerable<Client>> GetByClientId(int id, int startIndex, int itemCount)
        {
            IQueryable<Client> query = _context.Clients

                .Where(u =>
                    u.Id == id
                ).Skip(startIndex).Take(itemCount);

            var clients = await query.ToListAsync();



            return clients;
        }

        //UPDATE
        public async Task<int> Update(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return client.Id;
        }

        //DELETE
        public async void Delete(Client clients)
        {
            _context.Clients.Remove(clients);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Client>> GetClients()
        {

            IEnumerable<Client> clients = _context.Clients
                .Include(b => b.Appointments);

            clients.ToList();

            return clients;
        }

        
    }
}
