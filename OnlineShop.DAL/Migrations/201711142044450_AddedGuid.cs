namespace OnlineShop.DAL.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class AddedGuid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Guid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Guid");
        }
    }
}
