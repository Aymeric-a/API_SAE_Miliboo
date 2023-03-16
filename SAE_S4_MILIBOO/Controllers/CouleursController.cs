using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CouleursController : ControllerBase
    {
        private readonly IDataRepositoryCouleur<Couleur> dataRepository;

        public CouleursController(IDataRepositoryCouleur<Couleur> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Produits
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Couleur>>> GetProduits()
        {
            var lesCouleurs = await dataRepository.GetAll();
            return lesCouleurs;
        }
    }
}
