using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rtl.TvMazeScrapper.Domain.Entity.Base;
using Rtl.TvMazeScrapper.Domain.Interfaces.Repository.Base;

namespace Rtl.TvMazeScrapper.Infrastructure.Data.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
{
    private readonly TvMazeDbContext _dbContext;
    public BaseRepository(TvMazeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<bool> AddAsync(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().AnyAsync(predicate);
    }

    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<int> CountAsync()
    {
        return await _dbContext.Set<T>().CountAsync();
    }
}