namespace OnlineShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedProductsCountFromOrder : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Count");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Count", c => c.Int(nullable: false));
        }
    }
}