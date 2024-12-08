namespace API.MerchPlus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsertablerenamedtoMembertablealongwithrelatedtables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserRole", newName: "MemberRole");
            RenameTable(name: "dbo.User", newName: "Member");
            RenameTable(name: "dbo.UserClaim", newName: "MemberClaim");
            RenameTable(name: "dbo.UserLogin", newName: "MemberLogin");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MemberLogin", newName: "UserLogin");
            RenameTable(name: "dbo.MemberClaim", newName: "UserClaim");
            RenameTable(name: "dbo.Member", newName: "User");
            RenameTable(name: "dbo.MemberRole", newName: "UserRole");
        }
    }
}
