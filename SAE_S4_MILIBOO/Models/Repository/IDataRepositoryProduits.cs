//using Microsoft.AspNetCore.Mvc;

//namespace SAE_S4_MILIBOO.Models.Repository
//{
//    public interface IDataRepositoryProduits<TEntity>
//    {
//        Task<ActionResult<IEnumerable<TEntity>>> GetAll();


//        Task<decimal> GetNumberPages();
//        Task<decimal> GetNumberPagesByCollection(int collectionId);
//        Task<decimal> GetNumberPagesByCategorie(int categorieId);
//        Task<decimal> GetNumberPagesByCouleur(int couleurId);
//        Task<decimal> GetNumberPagesByPrix(double minprix, double maxprix);
//        Task<decimal> GetNumberPagesByCategorieAndCouleur(int collectionId, int couleurId);
//        Task<decimal> GetNumberPagesByCategorieAndPrix(int categorieId, double minprix, double maxprix);
//        Task<decimal> GetNumberPagesByCouleurAndPrix(int couleurId, double minprix, double maxprix);
//        Task<decimal> GetNumberPagesByCouleurAndPrixAndCategorie(int couleurId, double minprix, double maxprix, int categorieId);




//        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPage(int page);
//        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCollection(int page, int collectionId);
//        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCategorie(int page, int categorieId);
//        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCouleur(int page, int couleurId);
//        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByPrix(int page, double minprix, double maxprix);
//        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCategorieAndCouleur(int page, int collectionId, int couleurId);
//        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCategorieAndPrix(int page, int categorieId, double minprix, double maxprix);
//        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCouleurAndPrix(int page, int couleurId, double minprix, double maxprix);
//        Task<ActionResult<IEnumerable<TEntity>>> GetAllByPageByCouleurAndPrixAndCategorie(int page, int couleurId, double minprix, double maxprix, int categorieId);


//        Task<ActionResult<TEntity>> GetByIdAsync(int id);
//        Task<ActionResult<TEntity>> GetByStringAsync(string libelle);
//        Task<ActionResult<IEnumerable<TEntity>>> GetByStockNull();


//        Task AddAsync(TEntity entity);
//        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
//        Task DeleteAsync(TEntity entity);

//    }
//}
