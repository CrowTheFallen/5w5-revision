using Jungle_DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jungle_DataAccess.Repository
{
  public class Repository<T> : IRepository<T> where T : class
  {

    private readonly JungleDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(JungleDbContext db)
    {
      _db = db;
      this.dbSet = _db.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
      await dbSet.AddAsync(entity);
    }

    public async Task<T> GetAsync(int id)
    {
      return await dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, bool isTracking = true)
    {
      IQueryable<T> query = dbSet;

      if (filter != null)
      {
        query = query.Where(filter);
      }

      if (includeProperties != null)
      {
        foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
          query = query.Include(includeProp);
        }
      }

      if (orderBy != null)
      {
         
        return orderBy(query).ToList();
      }
      return query.ToList();
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool isTracking = true)
    {
      IQueryable<T> query = dbSet;

      if (filter != null)
      {
        query = query.Where(filter);
      }

      if (includeProperties != null)
      {
        foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
          query = query.Include(includeProp);
        }
      }


      return query.FirstOrDefault();
    }

    public async Task RemoveAsync(int id)
    {
      T entity = dbSet.Find(id);
      await RemoveAsync(entity);
    }

    public async Task RemoveAsync(T entity)
    {   
       
        dbSet.Remove(entity);

    }

        public async Task RemoveRangeAsync(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
