namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddDiscountCodeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DiscountCodes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(),
                    DiscountPercentage = c.Int(nullable: false),
                    UserId = c.String(maxLength: 128),
                    IsUsed = c.Boolean(nullable: false),
                    ExpiryDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.DiscountCodes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.DiscountCodes", new[] { "UserId" });
            DropTable("dbo.DiscountCodes");
        }
    }

}
