using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bonos.Models
{
    public class Periodo
    {
        [Key]
        public int Id { get; set; }
        public int N { get; set; }
        public string plazoGracia { get; set; }
        public double? bono { get; set; }
        public double? cupon { get; set; }
        public double? cuota { get; set; }
        public double? amortizacion { get; set; }
        public double? prima { get; set; }
        public double? escudo { get; set; }
        public double? flujoEmisor { get; set; }
        public double? flujoEmisorEscudo { get; set; }
        public double? flujoBonista { get; set; }
        public double? flujoActivo { get; set; }
        public double? flujoActivoPlazo { get; set; }
        public double? factorConvexidad { get; set; }
    }
}