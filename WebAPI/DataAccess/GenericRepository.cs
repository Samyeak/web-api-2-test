using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace WebAPI.DataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task Add(T item);
    }
    public abstract class GenericRepository<T> : BaseRepository, IGenericRepository<T> where T : class
    {
        protected DbSet<T> entities;
        public GenericRepository()
        {
            //entities = Context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll()
        {
            return entities;
        }
        public async Task<T> GetById(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task Add(T item)
        {
            entities.Add(item);
            await Context.SaveChangesAsync();
        }
    }
}