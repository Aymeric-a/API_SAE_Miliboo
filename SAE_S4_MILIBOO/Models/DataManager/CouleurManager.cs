﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class CouleurManager : IDataRepositoryCouleur<Couleur>
    {

        readonly MilibooDBContext? milibooDBContext;

        public CouleurManager() { }

        public CouleurManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public async Task<ActionResult<IEnumerable<Couleur>>> GetAll()
        {
            return await milibooDBContext.Couleurs.ToListAsync<Couleur>();
        }

        public string GetCodeCouleur(int id)
        {
            throw new NotImplementedException();
        }

        public int GetIdByLibelle(string libelle)
        {
            throw new NotImplementedException();
        }


        public async Task<ActionResult<IEnumerable<Couleur>>> GetCouleurofProduit(int produitId)
        {
            var lesVariantes = await milibooDBContext.Variantes.Where<Variante>(var => var.IdProduit == produitId).ToListAsync();
            List<Couleur> lesCouleurs = new List<Couleur>();
            foreach (Variante var in lesVariantes)
            {
                lesCouleurs.Add(await milibooDBContext.Couleurs.FirstAsync<Couleur>(c => c.IdCouleur == var.IdCouleur));
            }

            return lesCouleurs;
        }
    }
}
