using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryLignePanier<TEntity>
    {
        Task AddAsync(LignePanier entity);

        Task DeleteAsync(LignePanier entity);

        Task<ActionResult<IEnumerable<LignePanier>>> GetLignePaniersByClientId(int idClient);

        Task<ActionResult<LignePanier>> GetByIdAsync(int id);

        Task UpdateAsync(LignePanier entityToUpdate, LignePanier entity);
    }
}
