using Microsoft.AspNetCore.Mvc;

namespace SAE_S4_MILIBOO.Models.Repository
{
    public interface IDataRepositoryProduits<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        Task<ActionResult<TEntity>> GetProduitById(int id);


        Task<decimal> GetNumberPages();
        Task<decimal> GetNumberPagesByCollection(int collectionId);
        Task<decimal> GetNumberPagesByCategorie(int categorieId);
        Task<decimal> GetNumberPagesByCouleur(int couleurId);
        Task<decimal> GetNumberPagesByPrixMini(double minprix);
        Task<decimal> GetNumberPagesByPrixMaxi(double maxprix);
        Task<decimal> GetNumberPagesByCategorieAndCouleur(int categorieId, int couleurId);
        //Task<decimal> GetNumberPagesByCategorieAndPrix(int categorieId, double minprix, double maxprix);
        //Task<decimal> GetNumberPagesByCouleurAndPrix(int couleurId, double minprix, double maxprix);
        //Task<decimal> GetNumberPagesByCouleurAndPrixAndCategorie(int couleurId, double minprix, double maxprix, int categorieId);




        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPage(int page);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCollection(int page, int collectionId);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCategorie(int page, int categorieId);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCouleur(int page, int couleurId);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByPrixMini(int page, double minprix);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByPrixMaxi(int page, double maxprix);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCategorieAndCouleur(int page, int categorieId, int couleurId);
        //Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCategorieAndPrix(int page, int categorieId, double minprix, double maxprix);
        //Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCouleurAndPrix(int page, int couleurId, double minprix, double maxprix);
        //Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCouleurAndPrixAndCategorie(int page, int couleurId, double minprix, double maxprix, int categorieId);


        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetByStringAsync(string libelle);
        //Task<ActionResult<IEnumerable<TEntity>>> GetByStockNull();


        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);

    }
}
