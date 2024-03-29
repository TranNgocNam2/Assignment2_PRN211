using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repo;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly Asm2Context _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(Asm2Context context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    // Updated Get method with pagination
    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        int? pageIndex = null, // Optional parameter for pagination (page number)
        int? pageSize = null)  // Optional parameter for pagination (number of records per page)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        // Implementing pagination
        if (pageIndex.HasValue && pageSize.HasValue)
        {
            // Ensure the pageIndex and pageSize are valid
            int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
            int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10; // Assuming a default pageSize of 10 if an invalid value is passed

            query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
        }

        return query.ToList();
    }


    public virtual TEntity GetById(object id)
    {
        return _dbSet.Find(id);
    }

   

    public virtual void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
        TEntity entityToDelete = _dbSet.Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
        _dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}