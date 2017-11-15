using OnlineShop.DAL;
using OnlineShop.DAL.Entities;
using OnlineShop.DAL.Repositories;
using OnlineShopApi.ViewModels.Product;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineShopApi.Controllers
{
	public class HomeController : BaseMvcController
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}

		[Authorize]
		public ActionResult ProductList(int id = 1)
		{
			IEnumerable<GetProductListViewModel> result = UnitOfWork.ProductRepo.Get().Select(p => new GetProductListViewModel
			{
				Id = p.Id,
				Name = p.Name,
				Category = p.Category.Name,
				Price = p.Price
			});
			return View(result.ToPagedList(id, 10));
		}

		public ActionResult Product(int id)
		{
			IProductRepo repo = UnitOfWork.ProductRepo;
			Product product = repo.FindById(id);
			return View(product);
		}

		[HttpGet]
		public ActionResult CreateProduct()
		{
			return View();
		}

		[HttpPost]
		public RedirectResult CreateProduct(CreateProductViewModel request)
		{
			IProductRepo repo = UnitOfWork.ProductRepo;
			try
			{
				Product product = new Product
				{
					Name = request.Name,
					Description = request.Description,
					Category = request.Category,
					Price = request.Price,
					Count = request.Count
				};
				repo.Create(product);
				return Redirect("/Home/ProductList");
			}
			catch
			{
				return null;
			}
		}

		[HttpGet]
		public ActionResult CategoryList()
		{
			ICategoryRepo repo = UnitOfWork.CategoryRepo;
			return View(repo.Get());
		}

		[HttpGet]
		public ActionResult Basket()
		{
			//IEnumerable<GetBasketViewModel> basket = UnitOfWork
			//	.UserRepo
			//	.Get()
			return View();
		}
	}
}
