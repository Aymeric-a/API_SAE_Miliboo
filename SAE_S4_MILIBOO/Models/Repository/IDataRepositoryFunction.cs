using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryFunction<TEntity>
    {
        Task<ActionResult<ClientsPlusCommandes>> GetInfosClientAndCommandes(string email);
        Task<ActionResult<Client>> ReplacePassword(string newPassword, int idClient);
    }
}
