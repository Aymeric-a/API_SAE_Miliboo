using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IDataRepositoryClient<Client> dataRepository;

        public ClientsController(IDataRepositoryClient<Client> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Clients
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            return await dataRepository.GetAll();
        }

        // GET: api/Clients/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await dataRepository.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<Client>> GetClientWithoutPassword(int id)
        {
            var clientvar = await dataRepository.GetByIdAsync(id);
            Client client = clientvar.Value;

            if (client == null)
            {
                return NotFound();
            }

            client.Password = null;

            return client;
        }

        // GET: api/Clients/DorseyAnne@gmail.com
        [HttpGet]
        [ActionName("GetByEmail")]
        public async Task<ActionResult<Client>> GetClientByEmail(string email)
        {
            var Client = await dataRepository.GetClientByEmail(email);

            if (Client == null)
            {
                return NotFound();
            }

            return Client;
        }

        // GET: api/Clients/0000000000
        [HttpGet]
        [ActionName("GetByPortable")]
        public async Task<ActionResult<Client>> GetClientByPortable(string portable)
        {
            var Client = await dataRepository.GetClientByPortable(portable);

            if (Client == null)
            {
                return NotFound();
            }

            return Client;
        }

        [HttpGet]
        [ActionName("GetByNomPrenom")]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClientsByNomPrenom(string recherche)
        {
            var listClients = await dataRepository.GetAllClientsByNomPrenom(recherche);

            if (listClients == null)
            {
                return NotFound();
            }

            return listClients;
        }

        [HttpGet]
        [ActionName("GetByNewsletterM")]
            public async Task<ActionResult<IEnumerable<Client>>> GetAllClientsNewsletterM()
            {
                var clients = await dataRepository.GetAllClientsNewsletterM();
                List<Client> listClients = clients.Value.ToList();

                if (listClients.Count == 0 || listClients == null)
                {
                    return NotFound();
                }

                return listClients;
            }


        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpGet]
        [ActionName("GetByNewsletterP")]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClientsNewsletterP()
        {
            var clients = await dataRepository.GetAllClientsNewsletterP();
            List<Client> listClients = clients.Value.ToList();

            if (listClients.Count == 0)
            {
                return NotFound();
            }

            return listClients;
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client Client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(Client);


            return CreatedAtAction("GetById", new { id = Client.ClientId }, Client);
        }

        // PUT: api/Client/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> PutAvis(int id, Client client)
        {
            if (id != client.ClientId)
            {
                return BadRequest();
            }

            var userToUpdate = await dataRepository.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(userToUpdate.Value, client);
                return NoContent();
            }
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);
            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);

            return NoContent();
        }

        //private bool ClientExists(int id)
        //{
        //    return _context.Clients.Any(e => e.ClientId == id);
        //}
    }
}
