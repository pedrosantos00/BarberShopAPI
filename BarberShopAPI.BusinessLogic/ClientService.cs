using BarberShopAPI.DAL;
using BarberShopAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopAPI.BusinessLogic
{
    public class ClientService
    {
        private readonly ClientRepository _clientRepository;
        public ClientService(ClientRepository clientRepository) => _clientRepository = clientRepository;

        //CRUD

        //CREATE
        public async Task<int> Create(Client client)
        {
            var id = 0;
            if (client == null) throw new ArgumentNullException(nameof(client));

            if (client != null) id = await _clientRepository.Create(client);

            return id;
        }
        //RETRIEVE
        public async Task<Client?> GetById(int id)
        {
            return await _clientRepository.GetById(id);
        }

        //RETRIEVE
        public async Task<Client?> GetByPhoneNumber(string phoneNumber)
        {
            return await _clientRepository.GetByPhoneNumber(phoneNumber);
        }



        //RETRIEVE All clients
        public async Task<List<Client>> GetClients()
        {
            IEnumerable<Client> clientsResult = await _clientRepository.GetClients();

            return clientsResult.ToList();
        }



        //RETRIEVE
        public async Task<Client?> GetAvailability(int id)
        {
            return await _clientRepository.GetById(id);
        }


        public async Task<Client> GetByName(string name)
        {
            return await _clientRepository.GetByName(name);
        }

        //public async Task<List<Client>> Search(string? filterword, int startIndex, int itemCount)
        //{
        //    IEnumerable<Client> clientList = await _clientRepository.Search(filterword, startIndex, itemCount);

        //    return clientList.ToList();
        //}

        public async Task<List<Client>> GetByUserId(int id, int startIndex, int itemCount)
        {
            IEnumerable<Client> clientList = await _clientRepository.GetByClientId(id, startIndex, itemCount);

            return clientList.ToList();
        }

        //DELETE
        public async Task<bool> Delete(int id)
        {
            Client? clientToDelete = await _clientRepository.GetById(id);
            if (clientToDelete != null)
            {
                _clientRepository.Delete(clientToDelete);
                return true;
            }
            else return false;
        }

        public async Task<int> UpdateRate(Client client)
        {
            return await _clientRepository.Update(client);
        }

        public async Task<int> UpdateFav(Client client)
        {
            return await _clientRepository.Update(client);
        }

        public async Task<int> UpdateComment(Client client)
        {
            return await _clientRepository.Update(client);
        }


    }
}
