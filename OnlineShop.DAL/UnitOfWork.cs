﻿using System;
using OnlineShop.DAL.Repositories;
using OnlineShop.DAL.Repositories.Implements;

namespace OnlineShop.DAL
{
	public class UnitOfWork : IUnitOfWork
	{
		private Context Context { get; set; } = new Context();
		private bool disposed = false;

		public virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					Context.Dispose();
				}
				disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public ICategoryRepo CategoryRepo() => new CategoryRepo(Context);
		public IOrderRepo OrderRepo() => new OrderRepo(Context);
		public IProductRepo ProductRepo() => new ProductRepo(Context);
		public IRoleRepo RoleRepo() => new RoleRepo(Context);
		public IUserRepo UserRepo() => new UserRepo(Context);
	}
}