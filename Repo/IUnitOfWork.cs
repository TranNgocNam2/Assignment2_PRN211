namespace Repo;

public interface IUnitOfWork
{
    IRepository<Account> AccountRepository { get; }
    IRepository<Category> CategoryRepository { get; }
    void Save();

}