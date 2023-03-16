using Microsoft.AspNetCore.Mvc;
using SAE_S4_MILIBOO.Models.EntityFramework;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryVariante<TEntity>
    {
        bool FindCouleur(int couleurId, ICollection<Variante> mesVariantes);
        
        //Task AddAsync(TEntity entity);
        //Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        //Task DeleteAsync(TEntity entity);
    }
}
