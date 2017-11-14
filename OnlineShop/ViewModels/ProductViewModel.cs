namespace OnlineShop.ViewModels
{
	public class ProductListViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public float Price { get; set; }

		public string Category { get; set; }
	}

	public class ProductViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public float Price { get; set; }

		public string Category { get; set; }

		public int OrdersCount { get; set; }

	}
}