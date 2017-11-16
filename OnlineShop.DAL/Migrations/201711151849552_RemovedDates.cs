namespace OnlineShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDates : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "CreateDt");
            DropColumn("dbo.Users", "BirthDt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "BirthDt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CreateDt", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
