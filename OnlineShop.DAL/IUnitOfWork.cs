using OnlineShop.DAL.Repositories;
using System;

namespace OnlineShop.DAL
{
	public interface IUnitOfWork : IDisposable
	{
		ICategoryRepo CategoryRepo();
		IOrderRepo OrderRepo();
		IProductRepo ProductRepo();
		IRoleRepo RoleRepo();
		IUserRepo UserRepo();
		void SaveChanges();
	}
}
