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
        [DisplayFormat(DataFormatString = @"{0:#\%}")]
        public double TEA { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = @"{0:#\%}")]
        public double TEP { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = @"{0:#\%}")]
        public double COK { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double costesInicialesEmisor { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double costesInicialesBonista { get; set; }

    }
}