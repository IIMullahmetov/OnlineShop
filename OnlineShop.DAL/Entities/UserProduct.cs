using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DAL.Entities
{
	public class UserProduct
	{
		[Key]
		[Column(Order = 0)]
		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }

		[Key]
		[Column(Order = 1)]
		public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }

		public int Count { get; set; }
	}
}
