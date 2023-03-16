using Microsoft.AspNetCore.Mvc;
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
            foreach (Variante variante in mesVariantes)
            {
                if (variante.IdCouleur == couleurId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
