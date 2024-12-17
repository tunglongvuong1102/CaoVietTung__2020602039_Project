namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToSupports : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supports", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Supports", "UserId");
            AddForeignKey("dbo.Supports", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supports", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Supports", new[] { "UserId" });
            DropColumn("dbo.Supports", "UserId");
        }
    }
}
