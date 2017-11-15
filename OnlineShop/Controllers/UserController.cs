using OnlineShop.DAL;
using OnlineShop.ViewModels;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
	public class UserController : Controller
    {
		private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public ActionResult ProductList(int id = 1)
		{
			IEnumerable<ProductListViewModel> result = unitOfWork.ProductRepo.Get().Select(p => new ProductListViewModel
			{
				Id = p.Id,
				Name = p.Name,
				Category = p.Category.Name,
				Price = p.Price
			});
			ViewData["categories"] = unitOfWork.CategoryRepo.Get();
			return View(result.ToPagedList(id, 10));
		}

		[HttpGet]
		public ActionResult Product(int id)
		{
			return View();
		}
    }
}