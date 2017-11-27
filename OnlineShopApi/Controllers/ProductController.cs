using OnlineShop.DAL.Entities;
using OnlineShop.DAL.Repositories;
using OnlineShopApi.ViewModels.Product;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineShopApi.Controllers
{
	[Authorize]
	public class ProductController : BaseMvcController
    {
		public ActionResult List(int id = 1)
		{
			List<Product> products = UnitOfWork.ProductRepo.Get().ToList();
			IEnumerable<GetProductListViewModel> result = products.Select(p => new GetProductListViewModel
			{
				Id = p.Id,
				Name = p.Name,
				Category = p.Category.Name,
				Price = p.Price
			});
			return View(result.ToPagedList(id, 10));
		}

		public ActionResult Detail(int id)
		{
			IProductRepo repo = UnitOfWork.ProductRepo;
			Product product = repo.FindById(id);
			return View(product);
		}

		[HttpGet]
		public ActionResult Create()
		{
			ViewData["categories"] = UnitOfWork.CategoryRepo.Get().ToList();
			return View();
		}

		[HttpPost]
		public ActionResult Create(CreateProductViewModel viewModel)
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
				return RedirectToAction("List", "Product");
			}
			catch
			{
				ViewData["categories"] = UnitOfWork.CategoryRepo.Get().ToList();
				return View(viewModel);
			}
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			try
			{
				IProductRepo repo = UnitOfWork.ProductRepo;
				Product product = repo.FindById(id) ?? throw new System.Exception();
				repo.Remove(product);
				return RedirectToAction("List");
			}
			catch
			{
				return RedirectToAction("List");
			}
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			ViewData["categories"] = UnitOfWork.CategoryRepo.Get().ToList();
			Product product = UnitOfWork.ProductRepo.FindById(id);
			EditProductViewModel viewModel = new EditProductViewModel
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				Count = product.Count,
				Category = product.CategoryId,
				Description = product.Description
			};
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(EditProductViewModel viewModel)
		{
			Product product = UnitOfWork.ProductRepo.FindById(viewModel.Id);
			product.Name = viewModel.Name;
			product.Description = viewModel.Description;
			product.Price = viewModel.Price;
			product.Count = viewModel.Count;
			UnitOfWork.SaveChanges();
			return RedirectToAction("List", "Product");
		}
	}
}