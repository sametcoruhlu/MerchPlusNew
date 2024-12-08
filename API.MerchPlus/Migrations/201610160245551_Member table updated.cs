namespace API.MerchPlus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Membertableupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "MemberTitleId", c => c.Int(nullable: false));
            AddColumn("dbo.Member", "AddressCityId", c => c.Int(nullable: false));
            AddColumn("dbo.Member", "AddressCoordinateX", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Member", "AddressCoordinateY", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Member", "DirectReportId", c => c.Int());
            DropColumn("dbo.Member", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Member", "Title", c => c.String(maxLength: 255));
            DropColumn("dbo.Member", "DirectReportId");
            DropColumn("dbo.Member", "AddressCoordinateY");
            DropColumn("dbo.Member", "AddressCoordinateX");
            DropColumn("dbo.Member", "AddressCityId");
            DropColumn("dbo.Member", "MemberTitleId");
        }
    }
}
