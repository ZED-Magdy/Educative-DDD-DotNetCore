using System.Collections.Generic;
using System.Threading.Tasks;
using Educative.Domain.Entity;

namespace Educative.Domain.Repository
{
    public interface IRepository<T>
    {
        Task<ICollection<T>> GetAll(Paginator paginator);
        Task<T> GetById(int id);
        Task Add(T t);
        Task saveChanges();

        void Delete(T entity);
    }
}
