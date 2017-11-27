using OnlineShop.DAL.Entities;
using OnlineShop.DAL.Repositories;
using OnlineShopApi.ViewModels.Categories;
using OnlineShopApi.ViewModels.Product;
using System.Linq;
using System.Web.Mvc;

namespace OnlineShopApi.Controllers
{
	[Authorize]
	public class CategoryController : BaseMvcController
    {
		[HttpGet]
		public ActionResult List(string name = null, int? category = 0)
		{
			User user = GetCurrentUser();
			if (user == null) return RedirectToAction("Login", "Account");
			ICategoryRepo repo = UnitOfWork.CategoryRepo;
			return View(repo.Get().Select(c => new GetCategoryListViewModel
			{
				Id = c.Id,
				Name = c.Name
			}));
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(CreateProductViewModel viewModel)
		{
			User user = GetCurrentUser();
			if (user == null) return RedirectToAction("Login", "Account");
			try
			{
				UnitOfWork.CategoryRepo.Create(new Category { Name = viewModel.Name });
				return RedirectToAction("List");
			}
			catch
			{
				return View(viewModel);
			}
		}

		[HttpGet]
		public ActionResult Edit()
		{
			User user = GetCurrentUser();
			if (user == null) return RedirectToAction("Login", "Account");
			return View();
		}

		[HttpPost]
		public ActionResult Edit(EditCategoryViewModel viewModel)
		{
			User user = GetCurrentUser();
			if (user == null) return RedirectToAction("Login", "Account");
			Category category = UnitOfWork.CategoryRepo.FindById(viewModel.Id);
			try
			{
				category.Name = viewModel.Name;
				return RedirectToAction("List");
			}
			catch
			{
				return View(viewModel);
			}
		}
	}
}