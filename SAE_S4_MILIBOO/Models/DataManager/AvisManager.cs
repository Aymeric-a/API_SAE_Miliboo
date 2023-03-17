using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using SAE_S4_MILIBOO.Models.Repository;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class AvisManager : IDataRepositoryAvis<Avis>
    {
        readonly MilibooDBContext? milibooDBContext;

        public AvisManager() { }

        public AvisManager(MilibooDBContext context)
        {
            milibooDBContext = context;
        }
        public async Task AddAsync(Avis entity)
        {
            await milibooDBContext.AddAsync(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Avis entity)
        {
            milibooDBContext.Avis.Remove(entity);
            await milibooDBContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Avis>>> GetAll()
        {
            return await milibooDBContext.Avis.ToListAsync<Avis>();
        }

        public async Task<ActionResult<Avis>> GetAvisById(int AvisId)
        {
            return await milibooDBContext.Avis.FirstOrDefaultAsync<Avis>(p => p.AvisId == AvisId);
        }

        public async Task<ActionResult<IEnumerable<Avis>>> GetAvisById(int AvisId)
        {
            return await milibooDBContext.Avis.FirstOrDefaultAsync<Avis>(p => p.Pro == AvisId);
        }
    }
}
