using CoffeeShop.Domain;

namespace CoffeeShop.Infraestructure.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly CoffeeShopDbContext _dbContext;
        public UnitOfWork(CoffeeShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            try
            {
                _dbContext.ChangeTracker.DetectChanges();

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao executar Commit no UnitOfWork", ex);
            }
        }
        public async Task ExecuteAsync(Func<Task> action)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await action();

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
