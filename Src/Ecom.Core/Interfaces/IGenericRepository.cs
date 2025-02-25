using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities;

namespace Ecom.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity<int>
    {
      Task <IReadOnlyList<T>> GetAllAsync();

        IEnumerable<T> GetAll();

        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>> [] includes);
        Task<T> GetByIdAsync(int Id, params Expression<Func<T, object>>[] includes);

        T GetById(int id);

        Task AddAsync (T entity);

        Task DeleteAsync (int id);

        Task UpdateAsync (int id, T entity);

        Task<int >CountAsync();
    }
}
