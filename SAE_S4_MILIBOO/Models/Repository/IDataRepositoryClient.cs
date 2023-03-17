using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryClient<TEntity>
    {
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<ActionResult<IEnumerable<Client>>> GetAll(int id);
        Task<ActionResult<Client>> GetClientByEmail(string email);
        Task<ActionResult<Client>> GetClientByPortable(string portable);
        Task<ActionResult<IEnumerable<Client>>> GetAllClientsByNomPrenom(string recherche);
        Task<ActionResult<IEnumerable<Client>>> GetAllClientsNewsletterM();
        Task<ActionResult<IEnumerable<Client>>> GetAllClientsNewsletterP();

    }
}
