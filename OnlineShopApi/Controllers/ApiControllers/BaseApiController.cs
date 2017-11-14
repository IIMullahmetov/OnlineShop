using OnlineShop.DAL;
using OnlineShop.DAL.Entities;
using System.Web.Http;

namespace OnlineShopApi.Controllers.ApiControllers
{
	public abstract class BaseApiController : ApiController
	{
		protected IUnitOfWork UnitOfWork { get; set; } = new UnitOfWork();
		protected User GetCurrentUser()
		{
			return null;
		}
	}
}