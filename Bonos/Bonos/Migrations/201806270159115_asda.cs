namespace Bonos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Calculo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        totalPeriodos = c.Int(nullable: false),
                        TEA = c.Double(nullable: false),
                        TEP = c.Double(nullable: false),
                        COK = c.Double(nullable: false),
                        costesInicialesEmisor = c.Double(nullable: false),
                        costesInicialesBonista = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Periodo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        N = c.Int(nullable: false),
                        plazoGracia = c.String(),
                        bono = c.Double(),
                        cupon = c.Double(),
                        cuota = c.Double(),
                        amortizacion = c.Double(),
                        prima = c.Double(),
                        escudo = c.Double(),
                        flujoEmisor = c.Double(nullable: false),
                        flujoEmisorEscudo = c.Double(),
                        flujoBonista = c.Double(),
                        flujoActivo = c.Double(),
                        flujoActivoPlazo = c.Double(),
                        factorConvexidad = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Bono", "Calculo_Id", c => c.Int());
            CreateIndex("dbo.Bono", "Calculo_Id");
            AddForeignKey("dbo.Bono", "Calculo_Id", "dbo.Calculo", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bono", "Calculo_Id", "dbo.Calculo");
            DropIndex("dbo.Bono", new[] { "Calculo_Id" });
            DropColumn("dbo.Bono", "Calculo_Id");
            DropTable("dbo.Periodo");
            DropTable("dbo.Calculo");
        }
    }
}
