using Microsoft.AspNetCore.Mvc;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryAdresse<TEntity>
    {
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
