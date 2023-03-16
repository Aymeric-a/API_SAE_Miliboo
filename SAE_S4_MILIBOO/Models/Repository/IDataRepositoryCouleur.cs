using Microsoft.AspNetCore.Mvc;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryCouleur<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();

    }
}
