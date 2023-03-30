using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.SpecialTypes;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryFunction<TEntity>
    {
        Task<ActionResult<ClientsPlusCommandes>> GetInfosClientAndCommandes(string email);
    }
}
