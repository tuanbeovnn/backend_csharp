using System.Linq.Expressions;
using Application.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Models;

namespace Application.Repositories.Core
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll();
        T? GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        bool Save();
        Task<bool> SaveAsync();
        void Update<TProperty>(T entity, Expression<Func<T, TProperty>> property, TProperty newValue);
    }

    public abstract class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        protected readonly BlogDbContext _context;
        private readonly DbSet<T> _table;

        public GenericRepository(BlogDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<PagedResult<T>> QueryAsync(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int page = 1, int size = 50)
        {
            var query = _table.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }


            query = orderBy != null
                ? orderBy(query)
                : query.OrderBy(p => p.Id);

            int totalCount = await query.CountAsync();

            var pagedQuery = query
                .Skip((page - 1) * size)
                .Take(page);

            // Execute query and retrieve results
            var results = await pagedQuery.ToListAsync();
            return new PagedResult<T>
            {
                Items = results,
                Total = totalCount,
                Page = page,
                PageSize = size
            };
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public T? GetById(object id)
        {
            return _table.Find(id);
        }

        public void Insert(T obj)
        {
            _table.Add(obj);
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void SoftDelete(T obj)
        {
            Update(obj, f => f.DataStatus, DataStatus.Deleted);
        }

        public void Update<TProperty>(T entity, Expression<Func<T, TProperty>> property, TProperty newValue)
        {
            var entry = _table.Entry(entity);
            entry.Property(property).CurrentValue = newValue;
            entry.Property(property).IsModified = true;
        }

        public EntityEntry<T> Entry(T entity) => _table.Entry(entity);


        public void Delete(object id)
        {
            var existing = _table.Find(id);
            if (existing != null)
            {
                _table.Remove(existing);
            }
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }

    public static class RepositoryExtensions
    {
        public static EntityEntry<T> SetProperty<T, TProperty>(this EntityEntry<T> entry,
            Expression<Func<T, TProperty>> property,
            TProperty newValue) where T : ModelBase
        {
            entry.Property(property).CurrentValue = newValue;
            entry.Property(property).IsModified = true;
            return entry;
        }
    }
}