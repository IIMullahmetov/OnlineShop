namespace OnlineShop.DAL.Migrations
{
	using OnlineShop.DAL.Entities;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<Context>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(Context context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.
			if (context.Set<Role>().Count() == 0)
			{
				context.Set<Role>().Add(new Role
				{
					Name = "Admin",
					Login = "admin",
					Password = "admin",
					Connection = "admin"
				});
				context.Set<Role>().Add(new Role
				{
					Name = "User",
					Login = "user",
					Password = "user",
					Connection = "user"
				});
				context.SaveChanges();
			}
		}
	}
}
