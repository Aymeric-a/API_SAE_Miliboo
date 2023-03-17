using Microsoft.AspNetCore.Mvc;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryProduits<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        Task<ActionResult<TEntity>> GetProduitById(int id);


        Task<decimal> GetNumberPagesByCollection(int collectionId);
        Task<decimal> GetNumberPagesByCategorie(int categorieId);
        Task<decimal> GetNumberPagesByCouleur(int categorieId, List<int> couleurId);
        Task<decimal> GetNumberPagesByPrixMini(int categorieId, double minprix);
        Task<decimal> GetNumberPagesByPrixMaxi(int categorieId, double maxprix);



        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCollection(int page, int collectionId);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCategorie(int page, int categorieId);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCouleur(int page, int categorieId, List<int> couleurId);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByPrixMini(int page, int categorieId, double minprix);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByPrixMaxi(int page, int categorieId, double maxprix);
        


        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetByStringAsync(string libelle);
        //Task<ActionResult<IEnumerable<TEntity>>> GetByStockNull();


        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);

    }
}
