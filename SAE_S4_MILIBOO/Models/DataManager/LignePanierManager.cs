using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class LignePanierManager : IDataRepositoryLignePanier<LignePanier>
    {


        readonly MilibooDBContext? milibooDBContext;

        public LignePanierManager() { }

        public LignePanierManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public async Task AddAsync(LignePanier entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(LignePanier entity)
        {
            milibooDBContext.LignePaniers.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<LignePanier>>> GetLignePaniersByClientId(int idClient)
        {
            return await milibooDBContext.LignePaniers.Where<LignePanier>(c => c.ClientId == idClient).ToListAsync();
        }

        public async Task<ActionResult<LignePanier>> GetByIdAsync(int id)
        {
            return await milibooDBContext.LignePaniers.FirstOrDefaultAsync<LignePanier>(c => c.LigneId == id);
        }

        public async Task UpdateAsync(LignePanier entityToUpdate, LignePanier entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.LigneId = entity.LigneId;
            entityToUpdate.ClientId = entity.ClientId;
            entityToUpdate.VarianteId = entity.VarianteId;
            entityToUpdate.Quantite = entity.Quantite;

            await milibooDBContext.SaveChangesAsync();
        }
    }
}
