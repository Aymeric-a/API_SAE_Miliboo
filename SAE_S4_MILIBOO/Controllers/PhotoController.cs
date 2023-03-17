using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Controllers
{
    public class PhotoController : ControllerBase
    {
        private readonly IDataRepositoryPhoto<Photo> dataRepository;

        public PhotoController(IDataRepositoryPhoto<Photo> dataRepo)
        {
            dataRepository = dataRepo;
        }


        // GET: api/Photos/5
        [HttpGet]
        [ActionName("GetById")]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            var Photo = await dataRepository.GetPhotoById(id);

            if (Photo == null)
            {
                return NotFound();
            }

            return Photo;
        }

        [HttpPost]
        public async Task<ActionResult<Photo>> PostPhoto(Photo Photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(Photo);


            return CreatedAtAction("GetById", new { id = Photo.PhotoId }, Photo);
        }

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var produit = await dataRepository.GetPhotoById(id);
            if (produit == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);

            return NoContent();
        }

        // GET: api/Avis/fauteuil
        [HttpGet]
        [ActionName("GetByAvis")]
        public async Task<ActionResult<IEnumerable<Photo>>> GetAllPhotosByAvis(int avisId)
        {
            var avis = await dataRepository.GetAllPhotosByAvis(avisId);

            if (avis == null)
            {
                return NotFound();
            }

            return avis;
        }
    }
}
