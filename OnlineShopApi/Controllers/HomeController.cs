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
			IEnumerable<Product> r = UnitOfWork.ProductRepo.Get();
			IEnumerable<GetProductListViewModel> result = UnitOfWork.ProductRepo.Get().Select(p => new GetProductListViewModel
			{
				Id = p.Id,
				Name = p.Name,
				Category = UnitOfWork.CategoryRepo.FindById(p.CategoryId).Name,
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
			ViewData["categories"] = UnitOfWork.CategoryRepo.Get().ToList();
			return View();
		}

		[HttpPost]
		public ActionResult CreateProduct(CreateProductViewModel viewModel)
		{
			Category category = UnitOfWork.CategoryRepo.FindById(viewModel.Category);
			try
			{
				Product product = new Product
				{
					Name = viewModel.Name,
					Price = viewModel.Price,
					Description = viewModel.Description,
					Count = viewModel.Count,
					Category = category
				};
				UnitOfWork.ProductRepo.Create(product);
				return RedirectToAction("ProductList", "Home");
			}
			catch
			{
				ViewData["categories"] = UnitOfWork.CategoryRepo.Get().ToList();
				return View(viewModel);
			}
		}
		
		[HttpGet]
		public ActionResult CategoryList()
		{
			ICategoryRepo repo = UnitOfWork.CategoryRepo;
			return View(repo.Get());
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
