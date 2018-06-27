namespace Bonos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatebono : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bono", "Usuario_Id", "dbo.Usuario");
            DropIndex("dbo.Bono", new[] { "Usuario_Id" });
            AlterColumn("dbo.Bono", "Usuario_Id", c => c.Int());
            CreateIndex("dbo.Bono", "Usuario_Id");
            AddForeignKey("dbo.Bono", "Usuario_Id", "dbo.Usuario", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bono", "Usuario_Id", "dbo.Usuario");
            DropIndex("dbo.Bono", new[] { "Usuario_Id" });
            AlterColumn("dbo.Bono", "Usuario_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Bono", "Usuario_Id");
            AddForeignKey("dbo.Bono", "Usuario_Id", "dbo.Usuario", "Id", cascadeDelete: true);
        }
    }
}
