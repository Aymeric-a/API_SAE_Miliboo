using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryCarteBancaire<TEntity>
    {
        Task AddAsync(CarteBancaire entity);
        Task<ActionResult<CarteBancaire>> GetByIdAsync(int id);
        Task DeleteAsync(CarteBancaire entity);
        Task<ActionResult<IEnumerable<CarteBancaire>>> GetAllCartesBancairesByClientId(int ClientId);
    }
}
