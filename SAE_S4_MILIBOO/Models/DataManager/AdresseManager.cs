using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System.Net;


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

        public async Task<ActionResult<IEnumerable<Adresse>>> GetAll()
        {
            return await milibooDBContext.Adresses.ToListAsync<Adresse>();
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

        public async Task<ActionResult<Adresse>> GetAdresseByIdClient(int idClient)
        {
            var adresseLivraison = await milibooDBContext.AdresseLivraisons.FirstOrDefaultAsync<AdresseLivraison>(adl => adl.ClientId == idClient);
            var adresse = await milibooDBContext.Adresses.FirstOrDefaultAsync<Adresse>(a => a.AdresseId == adresseLivraison.AdresseId);

            DeleteAllCycles deleteAllCycles = new DeleteAllCycles(milibooDBContext);
            return deleteAllCycles.DeleteAllCyclesFunction(adresse);    
        }

        public async Task<int> GetAdresseByValues(string numero, string rue, string cp)
        {
            var adresseToCheck = await milibooDBContext.Adresses.FirstOrDefaultAsync<Adresse>(ad => ad.Cp == cp && ad.Rue == rue && ad.Numero == numero);

            if (adresseToCheck != null)
            {
                return adresseToCheck.AdresseId;
            }
            else
            {
                return -1;
            }
        }

        public async Task AddAsyncWithClient(Adresse entity, int clientId)
        {
            AdresseLivraisonManager adlmanager = new AdresseLivraisonManager(milibooDBContext);
            
            await milibooDBContext.AddAsync(entity);
            
            await milibooDBContext.SaveChangesAsync();
            AdresseLivraison adl = new AdresseLivraison();
            adl.ClientId = clientId;
            adl.AdresseId = entity.AdresseId;
            
            await adlmanager.AddAsync(adl);
        }
    }
}
