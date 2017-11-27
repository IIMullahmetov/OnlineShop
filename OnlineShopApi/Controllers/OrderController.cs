﻿using OnlineShop.DAL.Entities;
using OnlineShopApi.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineShopApi.Controllers
{
	public class OrderController : BaseMvcController
    {
        public ActionResult List()
		{
			User user = GetCurrentUser();
			List<GetOrderListViewModel> orders = user.Orders.Select(o => new GetOrderListViewModel
			{
				Id = o.Id,
				Count = o.Products.Count,
				CreateDt = o.CreateDt,
				//TODO !!!
				//Price = o.Products.
			}).ToList();
			return View(orders);
		}

		public ActionResult Detail(int id)
		{
			List<Product> orderProducts = UnitOfWork.OrderRepo.FindById(id).Products.ToList();
			IEnumerable<GetOrderDetailViewModel> details = orderProducts.Select(p => new GetOrderDetailViewModel
			{
				Id = p.Id,
				Name = p.Name,
				Price = p.Price
			});
			return View(details);
		}
    }
}