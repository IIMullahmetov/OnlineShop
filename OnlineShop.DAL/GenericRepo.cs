using OnlineShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OnlineShop.DAL
{
	public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : BaseEntity
	{
		private DbContext _context;
		private DbSet<TEntity> _dbSet;

		public GenericRepo(DbContext context)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
		}

		public IEnumerable<TEntity> Get() => _dbSet.AsNoTracking();

		public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
			=> _dbSet.AsNoTracking().Where(predicate);


		public IEnumerable<TEntity> Get(int page) => _dbSet.OrderBy(t => t.Id).Skip((page - 1) * 10).Take(10);


		public TEntity FindById(int id) => _dbSet.Find(id);

		public void Create(TEntity item)
		{
			_dbSet.Add(item);
			_context.SaveChanges();
		}

		public void Remove(TEntity item)
		{
			_dbSet.Remove(item);
			_context.SaveChanges();
		}

		public int Count() => _dbSet.Count();
	}
}
