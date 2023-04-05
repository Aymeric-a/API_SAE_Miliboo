﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class LigneCommandeManager : IDataRepositoryLigneCommande<LigneCommande>
    {
        readonly MilibooDBContext? milibooDBContext;

        public LigneCommandeManager() { }

        public LigneCommandeManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public async Task AddAsync(LigneCommande entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public Task DeleteAsync(LigneCommande entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<LigneCommande>>> GetByCommande(int idCommande)
        {
            var lignesCommande = await milibooDBContext.LigneCommandes.Where<LigneCommande>(lcm => lcm.CommandeId == idCommande).ToListAsync();

            DeleteAllCycles delete = new DeleteAllCycles(milibooDBContext);

            lignesCommande = delete.ChargeComposants(lignesCommande, new List<string>() { "Variante" });
            foreach(LigneCommande lcom in lignesCommande)
            {
                lcom.VarianteLigneCommandeNavigation.LignesCommandeVarianteNavigation = null;
            }

            return lignesCommande;
        }

        public Task UpdateAsync(LigneCommande entityToUpdate, LigneCommande entity)
        {
            throw new NotImplementedException();
        }
    }
}
