using Microsoft.AspNetCore.Mvc;
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
    }
}
