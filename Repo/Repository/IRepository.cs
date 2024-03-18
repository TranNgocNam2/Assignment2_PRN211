using System.Linq.Expressions;

namespace Repo;

public interface IRepository<T>
{
        void Insert(T entity);
        void Delete(T entity);
        void Delete(object id);
        void Update(T entity);

        T GetById(object id);
        
        public IEnumerable<T> Get( Expression<Func<T, bool>>? filter,  Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, string includeProperties,  int? pageIndex, int? pageSize);
    
}