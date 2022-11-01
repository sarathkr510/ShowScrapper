using System.Linq.Expressions;
using Rtl.TvMazeScrapper.Domain.Entity.Base;

namespace Rtl.TvMazeScrapper.Domain.Interfaces.Repository.Base;

public interface IBaseRepository<T> where T : EntityBase
{
    Task<bool> AddAsync(T entity);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync();
}