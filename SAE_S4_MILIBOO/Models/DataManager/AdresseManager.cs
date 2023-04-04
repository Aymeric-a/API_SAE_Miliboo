using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;


namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class AdresseManager : IDataRepositoryAdresse<Adresse>
    {
        readonly MilibooDBContext? milibooDBContext;

        public AdresseManager() { }

        public AdresseManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public async Task AddAsync(Adresse entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Adresse entity)
        {
            milibooDBContext.Adresses.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<Adresse>> GetByIdAsync(int id)
        {
            return await milibooDBContext.Adresses.FirstOrDefaultAsync<Adresse>(c => c.AdresseId == id);
        }

        public async Task UpdateAsync(Adresse entityToUpdate, Adresse entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.AdresseId = entity.AdresseId;
            entityToUpdate.Cp = entity.Cp;
            entityToUpdate.Numero = entity.Numero;
            entityToUpdate.Pays = entity.Pays;
            entityToUpdate.Remarque = entity.Remarque;
            entityToUpdate.Rue = entity.Rue;
            entityToUpdate.TelFixe = entity.TelFixe;
            entityToUpdate.Ville = entity.Ville;

            await milibooDBContext.SaveChangesAsync();
        }
    }
}
