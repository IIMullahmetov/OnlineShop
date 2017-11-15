using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.DAL.Entities
{
	public class Product : BaseEntity
	{
		public Product()
		{
			Orders = new HashSet<OrderProduct>();
			Users = new HashSet<UserProduct>();
		}

		public string Name { get; set; }

		[MaxLength(length: 128, ErrorMessage = "Длина не должна превышать 128 символов")]
		public string Description { get; set; }

		public float Price { get; set; }

		public int Count { get; set; }

		public int CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		public virtual Category Category { get; set; }

		public virtual ICollection<OrderProduct> Orders { get; set; }

		public virtual ICollection<UserProduct> Users { get; set; }
	}
}
