using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class LigneCommandeManager : IDataRepositoryLigneCommande<Commande>
    {
        readonly MilibooDBContext? milibooDBContext;

        public LigneCommandeManager() { }

        public LigneCommandeManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public Task AddAsync(Commande entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Commande entity)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Commande>>> GetByCommande(int idCommande)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Commande entityToUpdate, Commande entity)
        {
            throw new NotImplementedException();
        }
    }
}
