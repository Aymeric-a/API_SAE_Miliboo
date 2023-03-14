//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SAE_S4_MILIBOO.Models.EntityFramework;
//using SAE_S4_MILIBOO.Models.Repository;

//namespace SAE_S4_MILIBOO.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class ProduitsController : ControllerBase
//    {
//        private readonly IDataRepositoryProduits<Produit> dataRepository;

//        public ProduitsController(IDataRepositoryProduits<Produit> dataRepo)
//        {
//            dataRepository = dataRepo;
//        }

//        // GET: api/Produits
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Produit>>> GetProduits()
//        {
//            return await dataRepository.GetAll();
//        }

//        // GET: api/Produits/5
//        [HttpGet("{id}")]
//        [ActionName("GetById")]
//        public async Task<ActionResult<Produit>> GetProduit(int id)
//        {
//            var produit = await dataRepository.GetByIdAsync(id);

//            if (produit == null)
//            {
//                return NotFound();
//            }

//            return produit;
//        }


//        // GET: api/Produits/fauteuil
//        [HttpGet("{id}")]
//        [ActionName("GetById")]
//        public async Task<ActionResult<Produit>> GetProduitByLibelle(string libelle)
//        {
//            var produit = await dataRepository.GetByStringAsync(libelle);

//            if (produit == null)
//            {
//                return NotFound();
//            }

//            return produit;
//        }

//        // PUT: api/Produits/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        [ActionName("Put")]
//        public async Task<IActionResult> PutProduit(int id, Produit produit)
//        {
//            if (id != produit.IdProduit)
//            {
//                return BadRequest();
//            }

//            var userToUpdate = await dataRepository.GetByIdAsync(id);
//            if (userToUpdate == null)
//            {
//                return NotFound();
//            }
//            else
//            {
//                await dataRepository.UpdateAsync(userToUpdate.Value, produit);
//                return NoContent();
//            }
//        }

//        // POST: api/Produits
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        [ActionName("Post")]
//        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            await dataRepository.AddAsync(produit);


//            return CreatedAtAction("GetById", new { id = produit.IdProduit }, produit);
//        }

//        // DELETE: api/Produits/5
//        [HttpDelete("{id}")]
//        [ActionName("Delete")]
//        public async Task<IActionResult> DeleteProduit(int id)
//        {
//            var produit = await dataRepository.GetByIdAsync(id);
//            if (produit == null)
//            {
//                return NotFound();
//            }

//            await dataRepository.DeleteAsync(produit.Value);

//            return NoContent();
//        }

//        //private bool ProduitExists(int id)
//        //{
//        //    return _context.Produits.Any(e => e.IdProduit == id);
//        //}
//    }
//}
