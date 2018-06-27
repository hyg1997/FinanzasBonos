namespace Bonos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbono : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        vnominal = c.Double(nullable: false),
                        vcomercial = c.Double(nullable: false),
                        años = c.Int(nullable: false),
                        frecuencia = c.Int(nullable: false),
                        diasAño = c.Int(nullable: false),
                        tipoInteres = c.String(nullable: false),
                        capitalizacion = c.Int(),
                        tasaInteres = c.Double(nullable: false),
                        tasaDescuento = c.Double(nullable: false),
                        impuestoRenta = c.Double(nullable: false),
                        pPrima = c.Double(nullable: false),
                        pEstructura = c.Double(nullable: false),
                        pColoca = c.Double(nullable: false),
                        pFlota = c.Double(nullable: false),
                        pCAVALI = c.Double(nullable: false),
                        nombre = c.String(nullable: false),
                        tipoActor = c.String(nullable: false),
                        Usuario_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Usuario_Id, cascadeDelete: true)
                .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bono", "Usuario_Id", "dbo.Usuario");
            DropIndex("dbo.Bono", new[] { "Usuario_Id" });
            DropTable("dbo.Bono");
        }
    }
}
