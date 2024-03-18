namespace Repo;

public class UnitOfWork : IUnitOfWork
{
    private readonly Asm2Context _context;
    private IRepository<Account>? _accountRepo;
    private IRepository<Category>? _categoryRepo;
    
    public UnitOfWork(Asm2Context context)
    {
        this._context = context;
    }

    public void Save()
    {
        _context.SaveChanges();
    }
    
    public IRepository<Account> AccountRepository
    {
        get
        {

            if (_accountRepo is null)
            {
                _accountRepo = new Repository<Account>(_context);
            }
            return _accountRepo;
        }
    }

    public IRepository<Category> CategoryRepository
    {
        get
        {
            if (_categoryRepo is null)
            {
                _categoryRepo = new Repository<Category>(_context);
            }

            return _categoryRepo;
        }
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose(); 
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}