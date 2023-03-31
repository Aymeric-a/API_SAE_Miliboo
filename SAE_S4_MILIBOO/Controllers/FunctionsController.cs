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
    public class FunctionsController : ControllerBase
    {
        private readonly IDataRepositoryFunction<ClientsPlusCommandes> dataRepository;
        private readonly IDataRepositoryClient<Client> dataRepository2;

        public FunctionsController(IDataRepositoryFunction<ClientsPlusCommandes> dataRepo, IDataRepositoryClient<Client> dataRepo2)
        {
            dataRepository = dataRepo;
            dataRepository2 = dataRepo2;
        }

        [HttpGet]
        [ActionName("GetInfosClientsAndCommandes")]
        async Task<ActionResult<ClientsPlusCommandes>> GetInfosClientAndCommandes(string email)
        {
            var client = await dataRepository.GetInfosClientAndCommandes(email);
            ClientsPlusCommandes c = client.Value;

            if (c == null)
            {
                return NotFound();
            }

            return c;
        }
        async Task<ActionResult<Client>> ReplacePassword(string oldPassword, string newPassword, int idClient)
        {
            var client = await dataRepository2.GetByIdAsync(idClient);
            Client c = client.Value;

            if (c.Password != oldPassword)
            {
                Response.StatusCode = 400;
                var codeError = Response.StatusCode;

                return BadRequest("Erreur " + codeError + " : Bad Request \nL'ancien mot de passe ne correspond pas a celui relié a ce compte");
            }

            if (oldPassword == newPassword)
            {
                Response.StatusCode = 400;
                var codeError = Response.StatusCode;

                return BadRequest("Erreur " + codeError + " : Bad Request \nL'ancien et le nouveau mot de passe ne peuvent pas être le même");
            }

            dataRepository.ReplacePassword(newPassword, idClient);

            if (c == null)
            {
                return NotFound();
            }

            return c;
        }
    }
}
