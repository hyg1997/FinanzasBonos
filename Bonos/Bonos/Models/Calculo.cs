using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bonos.Models
{
    public class Calculo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int totalPeriodos { get; set; }
        [Required]
        public double TEA { get; set; }
        [Required]
        public double TEP { get; set; }
        [Required]
        public double COK { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double costesInicialesEmisor { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double costesInicialesBonista { get; set; }
        public double duracion { get; set; }
        public double convexidad { get; set; }
        public double total { get; set; }
        public double duracionModificada { get; set; }
        [DataType(DataType.Currency)]
        public double precioActual { get; set; }
        [DataType(DataType.Currency)]
        public double utilidad { get; set; }
        public double? TCEA { get; set; }
        public double? TCEAEmisor { get; set; }
        public double? TREA { get; set; }
    }
}