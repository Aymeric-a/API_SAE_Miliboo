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
    public class ListeSouhaitsController : ControllerBase
    {
        private readonly IDataRepositoryListeSouhait<ProduitListe> dataRepository;

        public ListeSouhaitsController(IDataRepositoryListeSouhait<ProduitListe> dataRepo)
        {
            dataRepository = dataRepo;
        }


        // GET: api/ListeSouhaits/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<ProduitListe>> GetListeSouhait(int id)
        {
            var ListeSouhait = await dataRepository.GetByIdAsync(id);

            if (ListeSouhait == null)
            {
                return NotFound();
            }

            return ListeSouhait;
        }

        // GET: api/ListeSouhaits/5
        [HttpGet]
        [ActionName("GetByVille")]
        public async Task<ActionResult<IEnumerable<ProduitListe>>> GetAllListeDeSouhaitsByClientId(int idClient)
        {
            var listeSouhait = await dataRepository.GetAllListeDeSouhaitsByClientId(idClient);

            if (listeSouhait == null)
            {
                return NotFound();
            }

            return listeSouhait;
        }

        // PUT: api/ListeSouhaits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListeSouhait(int id, ProduitListe ListeSouhait)
        {
            if (id != ListeSouhait.ListeId)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, ListeSouhait);
                return NoContent();
            }
        }

        // POST: api/ListeSouhaits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProduitListe>> PostListeSouhait(ProduitListe ListeSouhait)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(ListeSouhait);


            return CreatedAtAction("GetById", new { id = ListeSouhait.ListeId }, ListeSouhait);
        }

        // DELETE: api/ListeSouhaits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListeSouhait(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);
            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);

            return NoContent();
        }

        //private bool ListeSouhaitExists(int id)
        //{
        //    return _context.ListeSouhaits.Any(e => e.ListeSouhaitId == id);
        //}
    }
}
