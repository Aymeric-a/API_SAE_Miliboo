using Microsoft.AspNetCore.Mvc;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryCategorie<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetParent(int id);
        Task<ActionResult<IEnumerable<TEntity>>> GetSousCategories(int id);
        //Task AddAsync(TEntity entity);
        //Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        //Task DeleteAsync(TEntity entity);
    }
}
