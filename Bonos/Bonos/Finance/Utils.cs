using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bonos.Finance
{
    public class Utils
    {
        public static double HallarTEA(double TNP, int diasAño, int capitalizacion)
        {
            double m = diasAño / capitalizacion;
            return Math.Round(Math.Pow(1 + (TNP / m), m) - 1, 9);
        }

        public static double HallarTEP(double Tasa, int diasAño, int frecuencia, int? capitalizacion)
        {
            double TEA;
            if (capitalizacion.HasValue)
            {
                TEA = HallarTEA(Tasa, diasAño, capitalizacion.Value);
            }
            else
            {
                TEA = Tasa;
            }
            double fraccion = (double)frecuencia / diasAño;
            return Math.Round(Math.Pow(1 + TEA, fraccion) - 1, 9);
        }

        public static double HallarCOK(double tasaDescuento, int diasAño, int frecuencia)
        {
            double fraccion = (double)frecuencia / diasAño;
            return Math.Round(Math.Pow(1 + tasaDescuento, fraccion) - 1, 9);
        }
    }
}