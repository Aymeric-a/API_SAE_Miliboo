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


        // GET: api/Clients/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var Client = await dataRepository.GetByIdAsync(id);

            if (Client == null)
            {
                return NotFound();
            }

            return Client;
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
        async Task<ActionResult<IEnumerable<Client>>> GetAllClientsByNomPrenom(string recherche)
        {
            var listClients = await dataRepository.GetAllClientsByNomPrenom(recherche);

            if (listClients == null)
            {
                return NotFound();
            }

            return listClients;
        }

        [HttpGet]
        [ActionName("GetByPortable")]
        async Task<ActionResult<IEnumerable<Client>>> GetAllClientsNewsletterM()
        {
            var listClients = await dataRepository.GetAllClientsNewsletterM();


            if (listClients == null)
            {
                return NotFound();
            } 

            return listClients;
        }

        //Task<ActionResult<Client>> GetAllClientsNewsletterM();
        //Task<ActionResult<Client>> GetAllClientsNewsletterP();


        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpGet]
        [ActionName("GetByPortable")]
        async Task<ActionResult<IEnumerable<Client>>> GetAllClientsNewsletterP()
        {
            var listClients = await dataRepository.GetAllClientsNewsletterP();

            if (listClients == null)
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
