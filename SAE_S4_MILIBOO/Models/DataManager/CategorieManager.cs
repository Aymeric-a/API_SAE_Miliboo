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
            return await milibooDBContext.Categories.ToListAsync<Categorie>();
        }

        public async Task<ActionResult<Categorie>> GetByIdAsync(int id)
        {
            return await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(cat => cat.Categorieid== id);
        }

        public async Task<ActionResult<Categorie>> GetParent(int id)
        {
            Categorie IdParent = await milibooDBContext?.Categories.FirstOrDefaultAsync<Categorie>(cat => cat.Categorieid == id);
            return await milibooDBContext.Categories.FirstOrDefaultAsync<Categorie>(cat => cat.Categorieid == IdParent.CategorieParentid);
                //if(aymeric is not null)
                //    goto ensimag
                //else
                //    goto this.PlayWith(trouducul)
        }

        public async Task<ActionResult<IEnumerable<Categorie>>> GetSousCategories(int id)
        {
            return await milibooDBContext.Categories.Where<Categorie>(cat => cat.CategorieParentid== id).ToListAsync<Categorie>();
        }
    }
}
