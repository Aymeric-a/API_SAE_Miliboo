using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class CategorieManager : IDataRepositoryCategorie<Categorie>
    {
        readonly MilibooDBContext? milibooDBContext;

        public CategorieManager() { }

        public CategorieManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public async Task<ActionResult<IEnumerable<Categorie>>> GetAll()
        {
            var categories = await milibooDBContext.Categories.ToListAsync<Categorie>();
            if (categories != null)
            {
                foreach( Categorie cat in categories) { cat.CategorieParentNavigation = null; }
            }
            return categories;
        }

        public async Task<ActionResult<Categorie>> GetByIdAsync(int id)
        {
            return await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(cat => cat.Categorieid== id);
        }

        public async Task<ActionResult<Categorie>> GetParent(int id)
        {
            Categorie? IdParent = await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(cat => cat.Categorieid == id);
            var leParent = await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(cat => cat.Categorieid == IdParent.CategorieParentid);
            if(leParent != null)
                leParent.SousCategoriesNavigation = null;
            return leParent;

        }

        public async Task<ActionResult<IEnumerable<Categorie>>> GetSousCategories(int id)
        {
            var lesCategories =  await milibooDBContext.Categories.Where<Categorie>(cat => cat.CategorieParentid== id).ToListAsync<Categorie>();
            if(lesCategories != null)
                foreach(Categorie cat in lesCategories) { cat.CategorieParentNavigation = null; } 
            return lesCategories;
        }
    }
}
