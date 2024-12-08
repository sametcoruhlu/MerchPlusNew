namespace API.MerchPlus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddednullablefieldstoMembertable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Member", "HiringDate", c => c.DateTime());
            AlterColumn("dbo.Member", "AddressCityId", c => c.Int());
            AlterColumn("dbo.Member", "AddressCoordinateX", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Member", "AddressCoordinateY", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Member", "AddressCoordinateY", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Member", "AddressCoordinateX", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Member", "AddressCityId", c => c.Int(nullable: false));
            AlterColumn("dbo.Member", "HiringDate", c => c.DateTime(nullable: false));
        }
    }
}
