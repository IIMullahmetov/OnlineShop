using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.DAL.Entities
{
	public class UserProduct
	{
		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }

		public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }

		public int Count { get; set; }
	}
}
