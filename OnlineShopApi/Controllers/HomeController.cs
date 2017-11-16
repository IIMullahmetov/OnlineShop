using OnlineShop.DAL.Entities;
using OnlineShop.DAL.Repositories;
using OnlineShopApi.ViewModels.Categories;
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
		
		[HttpGet]
		public ActionResult CategoryList()
		{
			ICategoryRepo repo = UnitOfWork.CategoryRepo;
			return View(repo.Get().Select(c => new GetCategoryListViewModel
			{
				Id = c.Id,
				Name = c.Name
			}));
		}

		[HttpGet]
		public ActionResult CreateCategory()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CreateCategory(CreateProductViewModel viewModel)
		{
			try
			{
				UnitOfWork.CategoryRepo.Create(new Category { Name = viewModel.Name });
				return RedirectToAction("CategoryList", "Home");
			}
			catch
			{
				return View(viewModel);
			}
		}

		[HttpGet]
		public ActionResult Basket()
		{
			User user = GetCurrentUser();
			return View(user.Products.Select(p => new GetBasketViewModel
			{
				Id = p.ProductId,
				Name = p.Product.Name,
				Count = p.Count,
				PricePerUnit = p.Product.Price
			}));
		}

		[HttpPost]
		public ActionResult Buy()
		{
			User user = GetCurrentUser();
			try
			{
				Order order = new Order()
				{
					Products = user.Products.Select(p => new OrderProduct
					{
						ProductId = p.ProductId,
						Count = p.Count
					}).ToList()
				};
				UnitOfWork.OrderRepo.Create(order);
				user.Products.Clear();
				UnitOfWork.SaveChanges();
				return RedirectToAction("ProductList", "Home");
			}
			catch
			{
				return RedirectToAction("Basket", "Home");
			}
		}


	}
}
