using System.Linq;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();
        TEntity FindById(object id);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity editedEntity);
        Task<TEntity> DeleteAsync(TEntity entity);
        void SaveChanges();
    }
}
