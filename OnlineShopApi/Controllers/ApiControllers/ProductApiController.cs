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
			IProductRepo repo = UnitOfWork.ProductRepo;
			Product product = repo.FindById(request.Id);
			try
			{
				if (user.Products.Any(p => p.ProductId == product.Id))
				{
					user.Products.First(p => p.ProductId == product.Id).Count++;
					UnitOfWork.SaveChanges();
				}
				else
				{
					user.Products.Add(new UserProduct { Product = product });
					UnitOfWork.SaveChanges();
				}
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("delete_product_from_basket")]
		public IHttpActionResult DeleteProductFromBasket([FromBody]DeleteProductFromBasketRequest request)
		{
			User user = GetCurrentUser();
			if (user == null) return Unauthorized();
			UserProduct basketProduct = user.Products.FirstOrDefault(p => p.ProductId == request.Id);
			if (basketProduct == null) return BadRequest();
			try
			{
				user.Products.Remove(basketProduct);
				UnitOfWork.SaveChanges();
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("delete_one_product_instance_from_basket")]
		public IHttpActionResult DeleteOneProductInstanceFromBasket([FromBody]DeleteOneProductInstanceFromBasketRequest request)
		{
			User user = GetCurrentUser();
			if (user == null) return Unauthorized();
			UserProduct basketProduct = user.Products.FirstOrDefault(p => p.ProductId == request.Id);
			if (basketProduct == null) return BadRequest();
			try
			{
				basketProduct.Count--;
				if(basketProduct.Count < 1)
				{
					user.Products.Remove(basketProduct);
				}
				UnitOfWork.SaveChanges();
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("clear_basket")]
		public IHttpActionResult ClearBasket()
		{
			User user = GetCurrentUser();
			if (user == null) return Unauthorized();
			try
			{
				user.Products.Clear();
				UnitOfWork.SaveChanges();
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}
	}
}