﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System.Windows;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class AvisManager : IDataRepositoryAvis<Avis>
    {
        readonly MilibooDBContext? milibooDBContext;

        public AvisManager() { }

        public AvisManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }
        public async Task AddAsync(Avis entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Avis entity)
        {
            milibooDBContext.Avis.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Avis>>> GetAll()
        {
            return await milibooDBContext.Avis.ToListAsync<Avis>();
        }

        public async Task<ActionResult<Avis>> GetAvisById(int AvisId)
        {
            return await milibooDBContext.Avis.FirstOrDefaultAsync<Avis>(p => p.AvisId == AvisId);
        }

        public async Task<ActionResult<IEnumerable<Avis>>> GetAvisByProduit(int produitId)
        {
            List<Variante> allVariantes = await milibooDBContext.Variantes.Where(p => p.IdProduit == produitId).ToListAsync();

            List<Avis> allAvis = new List<Avis>();
            foreach (var v in allVariantes)
            {
                List<Avis> avis = await milibooDBContext.Avis.Where<Avis>(p => p.VarianteId == v.IdVariante).ToListAsync();
                foreach (Avis a in avis)
                {
                    a.VarianteAvisNavigation = null;
                    allAvis.Add(a);
                }
            }

            Console.WriteLine(allAvis.Count.ToString());

            return allAvis;
        }

        public async Task<ActionResult<IEnumerable<Avis>>> GetAvisByVariante(int varianteId)
        {
            return await milibooDBContext.Avis.Where<Avis>(p => p.VarianteId == varianteId).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Avis>>> GetAvisByClient(int clientId)
        {
            return await milibooDBContext.Avis.Where<Avis>(p => p.ClientId == clientId).ToListAsync();
        }

        public async Task UpdateAsync(Avis entityToUpdate, Avis entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.AvisId = entity.AvisId;
            entityToUpdate.VarianteId = entity.VarianteId;
            entityToUpdate.ClientId = entity.ClientId;
            entityToUpdate.AvisTitre = entity.AvisTitre;
            entityToUpdate.AvisTexte = entity.AvisTexte;
            entityToUpdate.AvisNote = entity.AvisNote;
            entityToUpdate.AvisDate = entity.AvisDate;

            await milibooDBContext.SaveChangesAsync();
        }
    }
}