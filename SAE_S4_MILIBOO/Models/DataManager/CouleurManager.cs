using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;
using System.Drawing;

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

        public async Task<string> GetCodeCouleur(int id)
        {
            string libelle = await milibooDBContext.Couleurs
            .Where(c => c.IdCouleur == id)
            .Select(c => c.Libelle)
            .FirstOrDefaultAsync();

            return GetHexCode(libelle);
        }

        public async Task<ActionResult<Couleur>> GetByIdAsync(int id)
        {
            return await milibooDBContext.Couleurs.FirstOrDefaultAsync(u => u.IdCouleur == id);
        }

        public static string GetHexCode(string colorName)
        {
            Color color = Color.FromName(colorName);
            return "#" + color.ToArgb().ToString("X8").Substring(2);
        }
    }
}
