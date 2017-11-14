using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.DAL.Entities
{
	public class BasketProduct 
	{
		[Key, Column(Order = 0)]
		public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }

		[Key, Column(Order = 1)]
		public int BasketId { get; set; }

		[ForeignKey("BasketId")]
		public virtual Basket Basket { get; set; }

		public int Count { get; set; }
	}
}
