namespace API.MerchPlus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MembertabelnowhasTitlefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "Title", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Member", "Title");
        }
    }
}
