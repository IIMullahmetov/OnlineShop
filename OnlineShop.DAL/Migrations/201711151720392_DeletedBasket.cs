namespace OnlineShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedBasket : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BasketProducts", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.Users", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.BasketProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.BasketProducts", new[] { "ProductId" });
            DropIndex("dbo.BasketProducts", new[] { "BasketId" });
            DropIndex("dbo.Users", new[] { "BasketId" });
            CreateTable(
                "dbo.UserProducts",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProductId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            DropColumn("dbo.Users", "BasketId");
            DropTable("dbo.BasketProducts");
            DropTable("dbo.Baskets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BasketProducts",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        BasketId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.BasketId });
            
            AddColumn("dbo.Users", "BasketId", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserProducts", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.UserProducts", new[] { "ProductId" });
            DropIndex("dbo.UserProducts", new[] { "UserId" });
            DropTable("dbo.UserProducts");
            CreateIndex("dbo.Users", "BasketId");
            CreateIndex("dbo.BasketProducts", "BasketId");
            CreateIndex("dbo.BasketProducts", "ProductId");
            AddForeignKey("dbo.BasketProducts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "BasketId", "dbo.Baskets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BasketProducts", "BasketId", "dbo.Baskets", "Id", cascadeDelete: true);
        }
    }
}
