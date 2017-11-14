using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.DAL.Entities
{
	public class Basket : BaseEntity
	{
		public Basket()
		{
			Users = new HashSet<User>();
			Products = new HashSet<BasketProduct>();
		}

		public virtual ICollection<User> Users { get; set; }

		public virtual ICollection<BasketProduct> Products { get; set; }
	}
}
