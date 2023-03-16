using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class CommandeManager : IDataRepositoryCommande<Commande>
    {
    readonly MilibooDBContext? milibooDBContext;

    public CommandeManager() { }

    public CommandeManager(MilibooDBContext context)
    {
        milibooDBContext = context;
    }


        public async Task AddAsync(Commande entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Commande entity)
        {
            milibooDBContext.Commandes.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Commande>>> GetAll()
        {
            return await milibooDBContext.Commandes.ToListAsync<Commande>();
        }

        public async Task<ActionResult<IEnumerable<Commande>>> GetAllCommandeByClientId(int clientId)
        {
            return await milibooDBContext.Commandes.Where<Commande>(c => c.ClientId == clientId).ToListAsync();
            
        }

        public async Task<ActionResult<IEnumerable<Commande>>> GetAllCommandeByEtat(int etatId)
        {
            return await milibooDBContext.Commandes.Where<Commande>(c => c.EtatId == etatId).ToListAsync();
        }

        public async Task<ActionResult<Commande>> GetByIdAsync(int id)
        {
            return await milibooDBContext.Commandes.FirstOrDefaultAsync(u => u.CommandeId == id);
        }


        public async Task UpdateAsync(Commande entityToUpdate, Commande entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.EtatId = entity.EtatId;
            entityToUpdate.AdresseId = entity.AdresseId;
            entityToUpdate.ClientId = entity.ClientId;
            entityToUpdate.Collecte = entity.Collecte;
            entityToUpdate.CommandeId = entity.CommandeId;
            entityToUpdate.Express = entity.Express;
            entityToUpdate.Instructions = entity.Instructions;
            entityToUpdate.PointsFideliteUtilises = entity.PointsFideliteUtilises;

            await milibooDBContext.SaveChangesAsync();
        }
    }
}
