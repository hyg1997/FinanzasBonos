namespace Bonos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allresults : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Calculo", "duracion", c => c.Double(nullable: false));
            AddColumn("dbo.Calculo", "convexidad", c => c.Double(nullable: false));
            AddColumn("dbo.Calculo", "total", c => c.Double(nullable: false));
            AddColumn("dbo.Calculo", "duracionModificada", c => c.Double(nullable: false));
            AddColumn("dbo.Calculo", "precioActual", c => c.Double(nullable: false));
            AddColumn("dbo.Calculo", "utilidad", c => c.Double(nullable: false));
            AddColumn("dbo.Calculo", "TCEA", c => c.Double());
            AddColumn("dbo.Calculo", "TCEAEmisor", c => c.Double());
            AddColumn("dbo.Calculo", "TREA", c => c.Double());
            AlterColumn("dbo.Periodo", "flujoEmisor", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Periodo", "flujoEmisor", c => c.Double(nullable: false));
            DropColumn("dbo.Calculo", "TREA");
            DropColumn("dbo.Calculo", "TCEAEmisor");
            DropColumn("dbo.Calculo", "TCEA");
            DropColumn("dbo.Calculo", "utilidad");
            DropColumn("dbo.Calculo", "precioActual");
            DropColumn("dbo.Calculo", "duracionModificada");
            DropColumn("dbo.Calculo", "total");
            DropColumn("dbo.Calculo", "convexidad");
            DropColumn("dbo.Calculo", "duracion");
        }
    }
}
