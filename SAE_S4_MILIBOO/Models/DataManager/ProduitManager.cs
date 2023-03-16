using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System.Linq;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class ProduitManager : IDataRepositoryProduits<Produit>
    {
        readonly MilibooDBContext? milibooDBContext;

        readonly VarianteManager varianteManager;

        private int nbrArticleParPage = 2;

        public ProduitManager() { }

        public ProduitManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public async Task AddAsync(Produit entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Produit entity)
        {
            milibooDBContext.Produits.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAll()
        {
            return await milibooDBContext.Produits.ToListAsync<Produit>();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPage(int page)
        {
            var rawData = await milibooDBContext.Produits.ToListAsync<Produit>();

            List<Produit> data = new List<Produit>();
            if (page * nbrArticleParPage > rawData.Count)
            {
                return data;
            }
            else
            {
                for (int i = (page - 1) * nbrArticleParPage; i < nbrArticleParPage * page; i++)
                {
                    data.Add(rawData[i]);
                }
            }
            return data;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCategorie(int page, int categorieId)
        {
            var rawData = await milibooDBContext.Produits.Where<Produit>(p => p.CategorieId == categorieId).ToListAsync();

            List<Produit> data = new List<Produit>();
            if (page * nbrArticleParPage > rawData.Count)
            {
                return data;
            }
            else
            {
                for (int i = (page - 1) * nbrArticleParPage; i < nbrArticleParPage * page; i++)
                {
                    data.Add(rawData[i]);
                }
            }
            return data;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCategorieAndCouleur(int page, int categorieId, int couleurId)
        {
            var rawData = await milibooDBContext.Produits.Where<Produit>(p => (p.CollectionId == categorieId))
                                                         .Where<Produit>(p => p.VariantesProduitNavigation.ToList().Exists(v => v.IdCouleur == couleurId))
                                                         .ToListAsync();

            List<Produit> data = new List<Produit>();
            if (page * nbrArticleParPage > rawData.Count)
            {
                return data;
            }
            else
            {
                for (int i = (page - 1) * nbrArticleParPage; i < nbrArticleParPage * page; i++)
                {
                    data.Add(rawData[i]);
                }
            }
            return data;
        }

        //public Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCategorieAndPrix(int page, int categorieId, double minprix, double maxprix)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCollection(int page, int collectionId)
        {
            var rawData = await milibooDBContext.Produits.Where<Produit>(p => p.CollectionId == collectionId).ToListAsync();

            List<Produit> data = new List<Produit>();
            if (page * nbrArticleParPage > rawData.Count)
            {
                return data;
            }
            else
            {
                for (int i = (page - 1) * nbrArticleParPage; i < nbrArticleParPage * page; i++)
                {
                    data.Add(rawData[i]);
                }
            }
            return data;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCouleur(int page, int couleurId)
        {
            var rawData = await milibooDBContext.Produits.Where<Produit>(p => p.VariantesProduitNavigation.ToList().Exists(v => v.IdCouleur == couleurId)).ToListAsync();

            List<Produit> data = new List<Produit>();
            if (page * nbrArticleParPage > rawData.Count)
            {
                return data;
            }
            else
            {
                for (int i = (page - 1) * nbrArticleParPage; i < nbrArticleParPage * page; i++)
                {
                    data.Add(rawData[i]);
                }
            }
            return data;
        }

        //public Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCouleurAndPrix(int page, int couleurId, double minprix, double maxprix)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCouleurAndPrixAndCategorie(int page, int couleurId, double minprix, double maxprix, int categorieId)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByPrixMini(int page, double minprix)
        {
            var rawData = await milibooDBContext.Produits.Where<Produit>(p => p.VariantesProduitNavigation.ToList().Exists(v => v.Prix >= minprix)).ToListAsync();

            List<Produit> data = new List<Produit>();
            if (page * nbrArticleParPage > rawData.Count)
            {
                return data;
            }
            else
            {
                for (int i = (page - 1) * nbrArticleParPage; i < nbrArticleParPage * page; i++)
                {
                    data.Add(rawData[i]);
                }
            }
            return data;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByPrixMaxi(int page, double maxprix)
        {
            var rawData = await milibooDBContext.Produits.Where<Produit>(p => p.VariantesProduitNavigation.ToList().Exists(v => v.Prix <= maxprix)).ToListAsync();

            List<Produit> data = new List<Produit>();
            if (page * nbrArticleParPage > rawData.Count)
            {
                return data;
            }
            else
            {
                for (int i = (page - 1) * nbrArticleParPage; i < nbrArticleParPage * page; i++)
                {
                    data.Add(rawData[i]);
                }
            }
            return data;
        }

        public async Task<ActionResult<Produit>> GetByIdAsync(int id)
        {
            return await milibooDBContext.Produits.FirstOrDefaultAsync(u => u.IdProduit == id);
        }

        //public async Task<ActionResult<IEnumerable<Produit>>> GetByStockNull()
        //{
        //    return await milibooDBContext.Produits.Where<Produit>(p => p.VariantesProduitNavigation.ToList().Exists(v => v.Stock == 0));
        //}

        public async Task<ActionResult<Produit>> GetByStringAsync(string libelle)
        {
            return await milibooDBContext.Produits.FirstOrDefaultAsync(u => u.Libelle.ToUpper() == libelle.ToUpper());
        }

        public async Task<decimal> GetNumberPages()
        {
            var nbrArticles = await milibooDBContext.Produits.CountAsync();
            
            return Math.Floor((decimal)(nbrArticles / nbrArticleParPage));
        }

        public async Task<decimal> GetNumberPagesByCategorie(int categorieId)
        {
            var nbrArticles = await milibooDBContext.Produits.Where<Produit>(p => p.CategorieId == categorieId).CountAsync();
            
            return Math.Floor((decimal)(nbrArticles / nbrArticleParPage));
        }

        public async Task<decimal> GetNumberPagesByCategorieAndCouleur(int categorieId, int couleurId)
        {
            var nbrArticles = await milibooDBContext.Produits.Where<Produit>(p => (p.CategorieId == categorieId))
                                                             .Where<Produit>(p => p.VariantesProduitNavigation.ToList().Exists(v => v.IdCouleur == couleurId))
                                                            .CountAsync();
            return Math.Floor((decimal)(nbrArticles / nbrArticleParPage));
        }

        //public Task<decimal> GetNumberPagesByCategorieAndPrix(int categorieId, double minprix, double maxprix)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<decimal> GetNumberPagesByCollection(int collectionId)
        {
            var nbrArticles = await milibooDBContext.Produits.Where<Produit>(p => p.CollectionId == collectionId).CountAsync();
            return Math.Floor((decimal)(nbrArticles / nbrArticleParPage));
        }

        public async Task<decimal> GetNumberPagesByCouleur(int couleurId)
        {
            return await milibooDBContext.Produits.Where<Produit>(p => varianteManager.FindCouleur(couleurId, p.VariantesProduitNavigation)).CountAsync();
            
        }

        //public Task<decimal> GetNumberPagesByCouleurAndPrix(int couleurId, double minprix, double maxprix)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<decimal> GetNumberPagesByCouleurAndPrixAndCategorie(int couleurId, double minprix, double maxprix, int categorieId)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<decimal> GetNumberPagesByPrixMini(double minprix)
        {
            var nbrArticles = await milibooDBContext.Produits.Where<Produit>(p => p.VariantesProduitNavigation.ToList().Exists(v => v.Prix >= minprix)).CountAsync();
            return Math.Floor((decimal)(nbrArticles / nbrArticleParPage));
        }

        public async Task<decimal> GetNumberPagesByPrixMaxi(double maxprix)
        {
            var nbrArticles = await milibooDBContext.Produits.Where<Produit>(p => p.VariantesProduitNavigation.ToList().Exists(v => v.Prix <= maxprix)).CountAsync();
            return Math.Floor((decimal)(nbrArticles / nbrArticleParPage));
        }

        public async Task UpdateAsync(Produit entityToUpdate, Produit entity)
        {
            milibooDBContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.CategorieId = entity.CategorieId;
            entityToUpdate.CollectionId = entity.CollectionId;
            entityToUpdate.DensiteAssise = entity.DensiteAssise;
            entityToUpdate.DensiteDossier = entity.DensiteDossier;
            entityToUpdate.DimAccoudoir = entity.DimAccoudoir;
            entityToUpdate.DimAssise = entity.DimAssise;
            entityToUpdate.DimColis = entity.DimColis;
            entityToUpdate.DimDeplie = entity.DimDeplie;
            entityToUpdate.DimDossier = entity.DimDossier;
            entityToUpdate.DimTotale = entity.DimTotale;
            entityToUpdate.HauteurPieds = entity.HauteurPieds;
            entityToUpdate.IdProduit = entity.IdProduit;
            entityToUpdate.InscructionsEntretien = entity.InscructionsEntretien;
            entityToUpdate.Libelle = entity.Libelle;
            entityToUpdate.Matiere = entity.Matiere;
            entityToUpdate.MatierePieds = entity.MatierePieds;
            entityToUpdate.PoidsColis = entity.PoidsColis;
            entityToUpdate.Revetement = entity.Revetement;
            entityToUpdate.TypeMousseAssise = entity.TypeMousseAssise;
            entityToUpdate.TypeMousseDossier = entity.TypeMousseDossier;

            await milibooDBContext.SaveChangesAsync();
        }
    }
}
