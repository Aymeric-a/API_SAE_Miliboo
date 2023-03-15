using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly IDataRepositoryProduits<Produit> dataRepository;

        public ProduitsController(IDataRepositoryProduits<Produit> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Produits
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduits()
        {
            return await dataRepository.GetAll();
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<Produit>> GetProduit(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/fauteuil
        [HttpGet]
        [ActionName("GetByLibelle")]
        public async Task<ActionResult<Produit>> GetProduitByLibelle(string libelle)
        {
            var produit = await dataRepository.GetByStringAsync(libelle);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetByAllByPage")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduitsByPage(int page)
        {
            var produit = await dataRepository.GetAllByPage(page);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetByAllByPageAndCategorie")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduitsByPageAndCategorie(int page, int categorieId)
        {
            var produit = await dataRepository.GetAllByPageByCategorie(page, categorieId);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetByAllByPageAndCouleur")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetByAllByPageAndCouleur(int page, int couleurId)
        {
            var produit = await dataRepository.GetAllByPageByCouleur(page, couleurId);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetByPageAndPrixMini")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduitsByPageAndPrixMini(int page, int min)
        {
            var produit = await dataRepository.GetAllByPageByPrixMini(page, min);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetByPageAndPrixMaxi")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduitsByPageAndPrixMaxi(int page, int max)
        {
            var produit = await dataRepository.GetAllByPageByPrixMaxi(page, max);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // GET: api/Produits/5
        [HttpGet]
        [ActionName("GetByAllByPageAndCollection")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetByAllByPageAndCollection(int page, int collectionId)
        {
            var produit = await dataRepository.GetAllByPageByCategorie(page, collectionId);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        [HttpGet]
        [ActionName("GetNumberPages")]
        public async Task<ActionResult<decimal>> GetNumberPages()
        {
            var nbrpages = await dataRepository.GetNumberPages();

            if (nbrpages == 0)
            {
                return NotFound();
            }

            return nbrpages;
        }

        [HttpGet]
        [ActionName("GetNumberPagesByCategorie")]
        public async Task<ActionResult<decimal>> GetNumberPagesByCategorie(int categorieId)
        {
            var nbrpages = await dataRepository.GetNumberPagesByCategorie(categorieId);

            if (nbrpages == 0)
            {
                return NotFound();
            }

            return nbrpages;
        }

        [HttpGet]
        [ActionName("GetNumberPagesByCouleur")]
        public async Task<ActionResult<decimal>> GetNumberPagesByCouleur(int couleurId)
        {
            var nbrpages = await dataRepository.GetNumberPagesByCouleur(couleurId);

            if (nbrpages == 0)
            {
                return NotFound();
            }

            return nbrpages;
        }

        [HttpGet]
        [ActionName("GetNumberPagesByPrixMaxi")]
        public async Task<ActionResult<decimal>> GetNumberPagesByPrixMaxi(int max)
        {
            var nbrpages = await dataRepository.GetNumberPagesByPrixMaxi(max);

            if (nbrpages == 0)
            {
                return NotFound();
            }

            return nbrpages;
        }

        [HttpGet]
        [ActionName("GetNumberPagesByPrixMini")]
        public async Task<ActionResult<decimal>> GetNumberPagesByPrixMin(int min)
        {
            var nbrpages = await dataRepository.GetNumberPagesByPrixMini(min);

            if (nbrpages == 0)
            {
                return NotFound();
            }

            return nbrpages;
        }

        [HttpGet]
        [ActionName("GetNumberPagesByCategorieAndCouleur")]
        public async Task<ActionResult<decimal>> GetNumberPagesByCategorieAndCouleur(int categorieId, int couleurId)
        {
            var nbrpages = await dataRepository.GetNumberPagesByCategorieAndCouleur(categorieId, couleurId);

            if (nbrpages == 0)
            {
                return NotFound();
            }

            return nbrpages;
        }


        [HttpGet]
        [ActionName("GetNumberPagesByCollection")]
        public async Task<ActionResult<decimal>> GetNumberPagesByCollection(int collectionId)
        {
            var nbrpages = await dataRepository.GetNumberPagesByCollection(collectionId);

            if (nbrpages == 0)
            {
                return NotFound();
            }

            return nbrpages;
        }

        // PUT: api/Produits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("Put")]
        public async Task<IActionResult> PutProduit(int id, Produit produit)
        {
            if (id != produit.IdProduit)
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
                await dataRepository.UpdateAsync(userToUpdate.Value, produit);
                return NoContent();
            }
        }

        // POST: api/Produits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("Post")]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(produit);


            return CreatedAtAction("GetById", new { id = produit.IdProduit }, produit);
        }

        // DELETE: api/Produits/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);
            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);

            return NoContent();
        }

        //private bool ProduitExists(int id)
        //{
        //    return _context.Produits.Any(e => e.IdProduit == id);
        //}
    }
}
