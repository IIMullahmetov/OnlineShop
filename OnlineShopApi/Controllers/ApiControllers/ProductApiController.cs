using OnlineShopApi.Requests;
using System.Web.Http;

namespace OnlineShopApi.Controllers.ApiControllers
{
	[RoutePrefix("product")]
	public class ProductApiController : BaseApiController
	{
		
		[HttpPost]
		[Route("add_product_to_basket")]
		public IHttpActionResult AddProductToBasket([FromBody]AddProductToBasketRequest request)
		{
			
			return Ok();
		}
	}
}