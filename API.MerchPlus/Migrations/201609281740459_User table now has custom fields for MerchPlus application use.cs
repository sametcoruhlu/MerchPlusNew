namespace API.MerchPlus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsertablenowhascustomfieldsforMerchPlusapplicationuse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "NameSurname", c => c.String(maxLength: 100));
            AddColumn("dbo.User", "SocialSecurityNumber", c => c.String(maxLength: 100));
            AddColumn("dbo.User", "DataNumber", c => c.String(maxLength: 100));
            AddColumn("dbo.User", "HiringDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.User", "LeavingDate", c => c.DateTime());
            AddColumn("dbo.User", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "Address", c => c.String(maxLength: 255));
            AddColumn("dbo.User", "DeviceModelId", c => c.Int());
            AddColumn("dbo.User", "IMEINumber", c => c.String(maxLength: 100));
            AddColumn("dbo.User", "ProfilePicturePath", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ProfilePicturePath");
            DropColumn("dbo.User", "IMEINumber");
            DropColumn("dbo.User", "DeviceModelId");
            DropColumn("dbo.User", "Address");
            DropColumn("dbo.User", "IsActive");
            DropColumn("dbo.User", "LeavingDate");
            DropColumn("dbo.User", "HiringDate");
            DropColumn("dbo.User", "DataNumber");
            DropColumn("dbo.User", "SocialSecurityNumber");
            DropColumn("dbo.User", "NameSurname");
        }
    }
}
