using OnlineShop.DAL.Entities;
using OnlineShopApi.Requests;
using System.Web.Http;

namespace OnlineShopApi.Controllers.ApiControllers
{
	[RoutePrefix("product")]
	public class ProductApiController : BaseApiController
	{
		
		[HttpPost]
		[Route("add_product_to_basket")]
		[Authorize]
		public IHttpActionResult AddProductToBasket([FromBody]AddProductToBasketRequest request)
		{
			User user = GetCurrentUser();
			return Ok();
		}
	}
}