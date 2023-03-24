using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryCouleur<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        int GetIdByLibelle(string libelle);
        string GetCodeCouleur(int id);
        Task<ActionResult<IEnumerable<Couleur>>> GetCouleurofProduit(int produitId)

    }
}
