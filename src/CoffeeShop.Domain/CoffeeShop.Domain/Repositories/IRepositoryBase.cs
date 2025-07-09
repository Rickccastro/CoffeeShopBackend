using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> ObterPorPropriedadeAsync(Expression<Func<T, bool>> predicate);
        Task<T> ObterPorIdAsync(Guid id);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task AdicionarAsync(T entity);
        Task AdicionarListaAsync(IEnumerable<T> entities);
        Task AtualizarAsync(T entity);
        Task RemoverAsync(T entity);
    }
}
