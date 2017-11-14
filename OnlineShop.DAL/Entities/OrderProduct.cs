using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.DAL.Entities
{
	public class OrderProduct 
	{
		[Key, Column(Order = 1)]
		public int OrderId { get; set; }

		[ForeignKey("OrderId")]
		public virtual Order Order { get; set; }

		[Key, Column(Order = 2)]
		public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }

		public int Count { get; set; }
	}
}
