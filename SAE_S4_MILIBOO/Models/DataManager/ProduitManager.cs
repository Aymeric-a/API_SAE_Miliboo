using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        readonly VarianteManager? varianteManager;

        readonly CategorieManager? categorieManager;

        private int nbrArticleParPage = 20;

        public ProduitManager() { }

        public ProduitManager(MilibooDBContext context)
        {
            milibooDBContext = context;
            varianteManager = new VarianteManager(context);
            categorieManager = new CategorieManager(context);
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

        public async Task<ActionResult<Produit>> GetProduitById(int produitId)
        {
            return await milibooDBContext.Produits.FirstOrDefaultAsync<Produit>(p => p.IdProduit == produitId);
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCategorie(int page, int categorieId)
        {
            var category = await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(p => p.Categorieid == categorieId);

            var allCategoriesChildsVar = await categorieManager.RecursivelyAllChildsCategories(category);
            List<Categorie> allCategoriesChilds = allCategoriesChildsVar.Value;

            List<Produit> allProducts = new List<Produit>();

            foreach (Categorie cat in allCategoriesChilds)
            {
                List<Produit> rawData = milibooDBContext.Produits.Where<Produit>(p => p.CategorieId == cat.Categorieid).ToList();

                foreach (Produit prd in rawData)
                {
                    prd.CategorieProduitNavigation = null;
                    allProducts.Add(prd);
                }
            }

            Console.WriteLine("DIS MOI : " + allProducts.Count());
            return DecouperListe(page, allProducts);
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCollection(int page, int collectionId)
        {
            var rawData = await milibooDBContext.Produits.Where<Produit>(p => p.CollectionId == collectionId).ToListAsync();

            return DecouperListe(page, rawData);
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCouleur(int page, int categorieId, List<int> couleurId )
        {
            var category = await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(p => p.Categorieid == categorieId);

            var allCategoriesChildsVar = await categorieManager.RecursivelyAllChildsCategories(category);
            List<Categorie> allCategoriesChilds = allCategoriesChildsVar.Value;

            List<int> lesIdProduits = await varianteManager.GetProduitsIdByCouleur(couleurId);

            List<Produit> resultProduit = new List<Produit>();
            for(int i=0; i<lesIdProduits.Count; i++)
            {
                Produit produit = await milibooDBContext.Produits.FirstOrDefaultAsync<Produit>(produit => produit.IdProduit == lesIdProduits[i]);

                if (allCategoriesChilds.Contains(await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(p => p.Categorieid == produit.CategorieId)))
                {
                    produit.VariantesProduitNavigation = null;
                    produit.CategorieProduitNavigation = null;
                    resultProduit.Add(produit);
                }
            }

            return DecouperListe(page, resultProduit.Where(p => p.CategorieId== categorieId).ToList());
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByPrixMini(int page, int categorieId, double minprix)
        {
            var rawData = await milibooDBContext.Produits.Where<Produit>(p => p.VariantesProduitNavigation.ToList().Exists(v => v.Prix >= minprix)).ToListAsync();

            return DecouperListe(page, rawData.Where(p => p.CategorieId == categorieId).ToList());
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByPrixMaxi(int page, int categorieId, double maxprix)
        {
            var rawData = await milibooDBContext.Produits.Where<Produit>(p => p.VariantesProduitNavigation.ToList().Exists(v => v.Prix <= maxprix)).ToListAsync();

            return DecouperListe(page, rawData.Where(p => p.CategorieId == categorieId).ToList());
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


        public async Task<decimal> GetNumberPagesByCategorie(int categorieId)
        {
            var nbrArticles = await milibooDBContext.Produits.Where<Produit>(p => p.CategorieId == categorieId).CountAsync();
            
            return Math.Floor((decimal)(nbrArticles / nbrArticleParPage));
        }


        public async Task<decimal> GetNumberPagesByCollection(int collectionId)
        {
            var nbrArticles = await milibooDBContext.Produits.Where<Produit>(p => p.CollectionId == collectionId).CountAsync();
            return Math.Floor((decimal)(nbrArticles / nbrArticleParPage));
        }


        public async Task<decimal> GetNumberPagesByCouleur(int categorieId, List<int> couleurId)
        {
            List<int> lesIdProduits = await varianteManager.GetProduitsIdByCouleur(couleurId);

            List<Produit> resultProduit = new List<Produit>();
            foreach (int id in lesIdProduits)
            {
                resultProduit.Add(await milibooDBContext.Produits.FirstOrDefaultAsync<Produit>(produit => produit.IdProduit == id));
            }

            return Math.Floor((decimal)(resultProduit.Where(p => p.CategorieId == categorieId).ToList().Count / nbrArticleParPage));
        }


        public async Task<decimal> GetNumberPagesByPrixMini(int categorieId, double minprix)
        {
            List<int> lesIdProduits = await varianteManager.GetProduitsIdByMinPrix(minprix);

            List<Produit> resultProduit = new List<Produit>();
            foreach (int id in lesIdProduits)
            {
                resultProduit.Add(await milibooDBContext.Produits.FirstOrDefaultAsync<Produit>(produit => produit.IdProduit == id));
            }
            return Math.Floor((decimal)(resultProduit.Where(p => p.CategorieId == categorieId).ToList().Count / nbrArticleParPage));
        }

        public async Task<decimal> GetNumberPagesByPrixMaxi(int categorieId, double maxprix)
        {
            List<int> lesIdProduits = await varianteManager.GetProduitsIdByMaxPrix(maxprix);

            List<Produit> resultProduit = new List<Produit>();
            foreach (int id in lesIdProduits)
            {
                resultProduit.Add(await milibooDBContext.Produits.FirstOrDefaultAsync<Produit>(produit => produit.IdProduit == id));
            }
            return Math.Floor((decimal)(resultProduit.Where(p => p.CategorieId == categorieId).ToList().Count / nbrArticleParPage));
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

        public ActionResult<IEnumerable<Produit>> DecouperListe(int page, List<Produit> rawData)
        {
            List<Produit> data = new List<Produit>();
            if (page * nbrArticleParPage > rawData.Count)
            {
                for (int i = (page - 1) * nbrArticleParPage; i < rawData.Count; i++)
                {
                    data.Add(rawData[i]);
                }
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
    }
}
