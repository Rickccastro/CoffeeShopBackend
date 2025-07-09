namespace CoffeeShop.Domain
{
    public interface IUnitOfWork
    {
        Task Commit();
        Task ExecuteAsync(Func<Task> action); 
    }
}
