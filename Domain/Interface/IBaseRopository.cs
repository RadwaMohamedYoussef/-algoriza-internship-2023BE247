using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IBaseRopository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindName(Expression<Func<T, bool>> match, string[] includes = null);
        Task<T> AddAsync(T item);
        T DeleteAsync(T item);
        T UpdateAsync(T item);
    }
}
