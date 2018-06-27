using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bonos.Models
{
    public class Bono
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double vnominal { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double vcomercial { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Áños debe ser mayor a 0")]
        public int años { get; set; }
        [Required]
        public int frecuencia { get; set; }
        [Required]
        public int diasAño { get; set; }
        [Required]
        public string tipoInteres { get; set; }
        public int? capitalizacion { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Áños debe ser mayor a 0")]
        public double tasaInteres { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Áños debe ser mayor a 0")]
        public double tasaDescuento { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Áños debe ser mayor a 0")]
        public double impuestoRenta { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Áños debe ser mayor a 0")]
        public double pPrima { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Áños debe ser mayor a 0")]
        public double pEstructura { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Áños debe ser mayor a 0")]
        public double pColoca { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Áños debe ser mayor a 0")]
        public double pFlota { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Áños debe ser mayor a 0")]
        public double pCAVALI { get; set; }
        [Required]
        public string nombre { get; set; }
        public Usuario Usuario { get; set; }
        public Calculo Calculo { get; set; }
        public List<Periodo> periodos { get; set; }
    }
}