using OnlineShop.DAL.Entities;
using OnlineShopApi.ViewModels.Product;
using System.Linq;
using System.Web.Mvc;

namespace OnlineShopApi.Controllers
{
	[Authorize]
	public class HomeController : BaseMvcController
	{

		public ActionResult Index()
		{
			User user = GetCurrentUser();
			if (user == null) return RedirectToAction("Login", "Account");
			ViewBag.Title = "Home Page";
			return View();
		}

		[HttpGet]
		public ActionResult Basket()
		{
			User user = GetCurrentUser();
			if (user == null) return RedirectToAction("Login", "Account");
			ViewBag.ProductCount = user.Products.Count;
			return View(user.Products.Distinct().Select(p => new GetBasketViewModel
			{
				Id = p.Id,
				Name = p.Name,
				Count = user.Products.Count(c => c.Id == p.Id),
				PricePerUnit = p.Price
			}));
		}

		[HttpPost]
		public ActionResult Buy()
		{
			User user = GetCurrentUser();
			if (user == null) return RedirectToAction("Login", "Account");
			try
			{
				Order order = new Order();
				foreach(Product product in user.Products)
				{
					order.Products.Add(product);
				}
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
