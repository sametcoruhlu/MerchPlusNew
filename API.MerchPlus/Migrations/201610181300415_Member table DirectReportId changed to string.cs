namespace API.MerchPlus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MembertableDirectReportIdchangedtostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Member", "DirectReportId", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Member", "DirectReportId", c => c.Int());
        }
    }
}
