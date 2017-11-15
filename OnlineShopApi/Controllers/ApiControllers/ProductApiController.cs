using OnlineShop.DAL.Entities;
using OnlineShop.DAL.Repositories;
using OnlineShopApi.Requests;
using System.Linq;
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
			if (user == null) return Unauthorized();
			IProductRepo repo = UnitOfWork.ProductRepo();
			Product product = repo.FindById(request.Id);
			try
			{
				if (user.Basket.Products.Any(p => p.ProductId == product.Id))
				{
					user.Basket.Products.First(p => p.ProductId == product.Id).Count++;
					UnitOfWork.SaveChanges();
				}
				else
				{
					user.Basket.Products.Add(new BasketProduct { Product = product });
					UnitOfWork.SaveChanges();
				}
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}

		public IHttpActionResult DeleteProductFromBasket([FromBody]DeleteProductFromBasketRequest request)
		{
			User user = GetCurrentUser();
			if (user == null) return Unauthorized();
			BasketProduct basketProduct = user.Basket.Products.FirstOrDefault(p => p.ProductId == request.Id);
			if(basketProduct == null) return BadRequest();

			return Ok();
		}
	}
}