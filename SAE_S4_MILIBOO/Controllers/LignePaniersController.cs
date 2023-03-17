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
    public class PaniersController : ControllerBase
    {
        private readonly IDataRepositoryPanier<Panier> dataRepository;

        public PaniersController(IDataRepositoryPanier<Panier> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Paniers/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<Panier>> GetPanier(int id)
        {
            var Panier = await dataRepository.GetByIdAsync(id);

            if (Panier == null)
            {
                return NotFound();
            }

            return Panier;
        }

        // GET: api/Paniers/5
        [HttpGet]
        [ActionName("GetByVille")]
        public async Task<ActionResult<IEnumerable<Panier>>> GetPanierByClient(int idClient)
        {
            var Panier = await dataRepository.GetPanierByClient(idClient);

            if (Panier == null)
            {
                return NotFound();
            }

            return Panier;
        }

        // PUT: api/Paniers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPanier(int id, Panier Panier)
        {
            if (id != Panier.PanierId)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, Panier);
                return NoContent();
            }
        }

        // POST: api/Paniers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Panier>> PostPanier(Panier Panier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(Panier);


            return CreatedAtAction("GetById", new { id = Panier.PanierId }, Panier);
        }

        // DELETE: api/Paniers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePanier(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);
            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);

            return NoContent();
        }

        //private bool PanierExists(int id)
        //{
        //    return _context.Paniers.Any(e => e.PanierId == id);
        //}
    }
}
