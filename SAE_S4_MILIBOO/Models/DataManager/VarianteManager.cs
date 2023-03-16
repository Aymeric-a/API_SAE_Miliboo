using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class VarianteManager : IDataRepositoryVariante<Variante>
    {
        readonly MilibooDBContext? milibooDBContext;

        public VarianteManager() { }

        public VarianteManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }

        public bool FindCouleur(int couleurId, ICollection<Variante> mesVariantes)
        {
            Console.WriteLine("test");
            foreach (Variante variante in mesVariantes)
            {
                if (variante.IdCouleur == couleurId)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<ActionResult<IEnumerable<Variante>>> GetAll()
        {
            return await milibooDBContext.Variantes.ToListAsync<Variante>();
        }

        public async Task<ActionResult<IEnumerable<Variante>>> GetAllByByCouleur(int couleurId)
        {
            return await milibooDBContext.Variantes.Where<Variante>(v => v.IdCouleur == couleurId).ToListAsync();
        }
    }
}
