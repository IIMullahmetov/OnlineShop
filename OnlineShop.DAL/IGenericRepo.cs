using OnlineShop.DAL.Entities;
using System;
using System.Collections.Generic;

namespace OnlineShop.DAL
{
	public interface IGenericRepo<TEntity> where TEntity : BaseEntity
	{
		void Create(TEntity item);
		TEntity FindById(int id);
		IEnumerable<TEntity> Get();
		IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
		void Remove(TEntity item);
	}
}
