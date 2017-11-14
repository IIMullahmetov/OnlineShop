namespace OnlineShopApi.Requests
{
	public class PostRequestsBase
	{
		public int Id { get; set; }
	}

	public class AddProductToBasketRequest : PostRequestsBase
	{
		public string Name { get; set; }
	}
}