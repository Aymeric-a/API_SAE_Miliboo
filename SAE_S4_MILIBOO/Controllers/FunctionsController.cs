using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using SAE_S4_MILIBOO.SpecialTypes;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FunctionsController : ControllerBase
    {
        private readonly IDataRepositoryFunction<ClientsPlusCommandes> dataRepository;

        public FunctionsController(IDataRepositoryFunction<ClientsPlusCommandes> dataRepo)
        {
            dataRepository = dataRepo;
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
    }
}
