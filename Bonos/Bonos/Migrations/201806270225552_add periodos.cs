namespace Bonos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addperiodos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Periodo", "Bono_Id", c => c.Int());
            CreateIndex("dbo.Periodo", "Bono_Id");
            AddForeignKey("dbo.Periodo", "Bono_Id", "dbo.Bono", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Periodo", "Bono_Id", "dbo.Bono");
            DropIndex("dbo.Periodo", new[] { "Bono_Id" });
            DropColumn("dbo.Periodo", "Bono_Id");
        }
    }
}
