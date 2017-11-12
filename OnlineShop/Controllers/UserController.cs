using System.Web.Mvc;

namespace OnlineShop.Controllers
{
	public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}