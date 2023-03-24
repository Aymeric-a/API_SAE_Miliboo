using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class ProduitManager : IDataRepositoryProduits<Produit>
    {
        readonly MilibooDBContext? milibooDBContext;

        readonly VarianteManager? varianteManager;

        readonly CategorieManager? categorieManager;

        private int nbrArticleParPage = 5;

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




        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByCategorie(int categorieId)
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

            return allProducts.ToList();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCategorie(int page, int categorieId)
        {
            var resultProduitVar = await GetAllByCategorie(categorieId);
            List<Produit> resultProduit = (List<Produit>)resultProduitVar.Value;

            return DecouperListe(page, resultProduit.ToList());
        }


        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByCollection(int collectionId)
        {
            return await milibooDBContext.Produits.Where<Produit>(p => p.CollectionId == collectionId).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCollection(int page, int collectionId)
        {

            var rawDataVar = await GetAllByCollection(collectionId);
            List<Produit> rawData = rawDataVar.Value.ToList();
            return DecouperListe(page, rawData);
        }



        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByCouleur(int page, int categorieId, List<int> couleurId )
        {
            var resultProduitVar = await GetAllByCouleur(categorieId, couleurId);
            List<Produit> resultProduit = (List<Produit>)resultProduitVar.Value;

            return DecouperListe(page, resultProduit.ToList());
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByCouleur(int categorieId, List<int> couleurId)
        {
            var category = await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(p => p.Categorieid == categorieId);

            var allCategoriesChildsVar = await categorieManager.RecursivelyAllChildsCategories(category);
            List<Categorie> allCategoriesChilds = allCategoriesChildsVar.Value;

            List<int> lesIdProduits = await varianteManager.GetProduitsIdByCouleur(couleurId);

            List<Produit> resultProduit = new List<Produit>();
            for (int i = 0; i < lesIdProduits.Count; i++)
            {
                Produit produit = await milibooDBContext.Produits.FirstOrDefaultAsync<Produit>(produit => produit.IdProduit == lesIdProduits[i]);

                if (allCategoriesChilds.Contains(await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(p => p.Categorieid == produit.CategorieId)))
                {
                    Console.WriteLine("CA DOIT CONTENIR SIDFNSLDINF");
                    produit.VariantesProduitNavigation = null;
                    produit.CategorieProduitNavigation = null;
                    resultProduit.Add(produit);
                }
            }

            return resultProduit.ToList();
        }


        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPrixMini(int categorieId, double minprix)
        {
            var category = await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(p => p.Categorieid == categorieId);

            var allCategoriesChildsVar = await categorieManager.RecursivelyAllChildsCategories(category);
            List<Categorie> allCategoriesChilds = allCategoriesChildsVar.Value;

            var rawData = await milibooDBContext.Produits.Where(p => p.VariantesProduitNavigation.Any(v => v.Prix >= minprix)).ToListAsync();

            List<Produit> resultProduit = new List<Produit>();

            foreach (Produit p in rawData)
            {
                if (ConvertCategoriesIntoIds(allCategoriesChilds).Contains(p.CategorieId))
                {
                    p.CategorieProduitNavigation = null;
                    p.VariantesProduitNavigation = null;
                    resultProduit.Add(p);
                }
            }

            Console.WriteLine("NOMBRE EST : " + resultProduit.Count);
            return resultProduit.ToList();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByPrixMini(int page, int categorieId, double minprix)
        {
            var resultProduitVar = await GetAllByPrixMini(categorieId, minprix);
            List<Produit> resultProduit = (List<Produit>)resultProduitVar.Value;

            return DecouperListe(page, resultProduit.ToList());
        }



        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPrixMaxi(int categorieId, double maxprix)
        {
            var category = await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(p => p.Categorieid == categorieId);

            var allCategoriesChildsVar = await categorieManager.RecursivelyAllChildsCategories(category);
            List<Categorie> allCategoriesChilds = allCategoriesChildsVar.Value;

            var rawData = await milibooDBContext.Produits.Where(p => p.VariantesProduitNavigation.Any(v => v.Prix <= maxprix)).ToListAsync();

            List<Produit> resultProduit = new List<Produit>();

            foreach (Produit p in rawData)
            {
                if (ConvertCategoriesIntoIds(allCategoriesChilds).Contains(p.CategorieId))
                {
                    p.CategorieProduitNavigation = null;
                    p.VariantesProduitNavigation = null;
                    resultProduit.Add(p);
                }
            }

            return resultProduit.ToList();
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllByPageByPrixMaxi(int page, int categorieId, double minprix)
        {
            var resultProduitVar = await GetAllByPrixMaxi(categorieId, minprix);
            List<Produit> resultProduit = (List<Produit>)resultProduitVar.Value;

            return DecouperListe(page, resultProduit.ToList());
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
            var nbrArticlesVar = await GetAllByCategorie(categorieId);
            List<Produit> nbrArticles = (List<Produit>)nbrArticlesVar.Value;

            return Math.Ceiling((decimal)(nbrArticles.Count() / nbrArticleParPage));
        }



        public async Task<decimal> GetNumberPagesByCollection(int collectionId)
        {
            var nbrArticlesVar = await GetAllByCollection(collectionId);
            List<Produit> nbrArticles = (List<Produit>)nbrArticlesVar.Value;

            return Math.Ceiling((decimal)(nbrArticles.Count() / nbrArticleParPage));
        }


        public async Task<decimal> GetNumberPagesByCouleur(int categorieId, List<int> couleurId)
        {
            var nbrArticlesVar = await GetAllByCouleur(categorieId, couleurId);
            List<Produit> nbrArticles = (List<Produit>)nbrArticlesVar.Value;

            return Math.Ceiling((decimal)(nbrArticles.Count() / nbrArticleParPage));
        }


        public async Task<decimal> GetNumberPagesByPrixMini(int categorieId, double minprix)
        {
            var nbrArticlesVar = await GetAllByPrixMini(categorieId, minprix);
            List<Produit> nbrArticles = (List<Produit>)nbrArticlesVar.Value;

            return Math.Ceiling((decimal)(nbrArticles.Count() / nbrArticleParPage));
        }

        public async Task<decimal> GetNumberPagesByPrixMaxi(int categorieId, double maxprix)
        {
            var nbrArticlesVar = await GetAllByPrixMaxi(categorieId, maxprix);
            List<Produit> nbrArticles = (List<Produit>)nbrArticlesVar.Value;

            return Math.Ceiling((decimal)(nbrArticles.Count() / nbrArticleParPage));
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

        public List<int> ConvertCategoriesIntoIds(List<Categorie> categories) 
        {
            List<int> allCategoriesInt = new List<int>();
            foreach (Categorie c in categories)
            {
                allCategoriesInt.Add(c.Categorieid);
            }

            return allCategoriesInt;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetBitchesForRemy(int? categorieId, int? collectionId, List<int>? couleurId, double? maxprix, double? minprix)
        {
            var productsAfterFilterCat = await GetAll();
            var productsAfterFilterCollection = await GetAll();
            var productsAfterFilterColors = await GetAll();
            var productsAfterFilterMaxPrice = await GetAll();
            var productsAfterFilterMinPrice = await GetAll();

            if (categorieId != null)
            {
                productsAfterFilterCat = await GetAllByCategorie((int)categorieId);
                if (couleurId != null)
                {
                    productsAfterFilterColors = await GetAllByCouleur((int)categorieId, couleurId);
                }
                if (maxprix != null)
                {
                    productsAfterFilterMaxPrice = await GetAllByPrixMaxi((int)categorieId, (int)maxprix);
                }
                if (minprix != null)
                {
                    productsAfterFilterMinPrice = await GetAllByPrixMini((int)categorieId, (int)minprix);
                }
            }
            if (collectionId != null)
            {
                productsAfterFilterCollection = await GetAllByCollection((int)collectionId);
            }

            List<Produit> productsAfterFilterCatList = productsAfterFilterCat.Value.ToList();
            List<Produit> productsAfterFilterCollectionList = productsAfterFilterCat.Value.ToList();
            List<Produit> productsAfterFilterColorsList = productsAfterFilterCat.Value.ToList();
            List<Produit> productsAfterFilterMaxPriceList = productsAfterFilterCat.Value.ToList();
            List<Produit> productsAfterFilterMinPriceList = productsAfterFilterCat.Value.ToList();

            List<Produit> finalList = (List<Produit>)productsAfterFilterCatList.Intersect(productsAfterFilterCollectionList);
            finalList = (List<Produit>)finalList.Intersect(productsAfterFilterColorsList);
            finalList = (List<Produit>)finalList.Intersect(productsAfterFilterMaxPriceList);
            finalList = (List<Produit>)finalList.Intersect(productsAfterFilterMinPriceList);

            return finalList;

        }
    }
}
