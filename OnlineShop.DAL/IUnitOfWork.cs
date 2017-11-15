using OnlineShop.DAL.Repositories;
using System;

namespace OnlineShop.DAL
{
	public interface IUnitOfWork : IDisposable
	{
		ICategoryRepo CategoryRepo { get; }
		IOrderRepo OrderRepo { get; }
		IProductRepo ProductRepo { get; }
		IRoleRepo RoleRepo { get; }
		IUserRepo UserRepo { get; }
		void SaveChanges();
	}
}
