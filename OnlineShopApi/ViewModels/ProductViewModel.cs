using OnlineShop.DAL.Entities;

namespace OnlineShopApi.ViewModels.Product
{
	public class GetProductListViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public float Price { get; set; }

		public string Category { get; set; }
	}

	public class GetProductViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public float Price { get; set; }

		public string Category { get; set; }

		public int OrdersCount { get; set; }
	}

	public class CreateProductViewModel
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public float Price { get; set; }

		public Category Category { get; set; }

		public int Count { get; set; }
	}

	public class GetBasketViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int Count { get; set; }

		public float PricePerUnit { get; set; }
	}
}