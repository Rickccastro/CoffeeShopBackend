using CoffeeShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly CoffeeShopDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(CoffeeShopDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AdicionarListaAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual async Task<T> ObterPorPropriedadeAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<T> ObterPorIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task AdicionarAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AtualizarAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual async Task RemoverAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<T> ObterPorIdStringAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
