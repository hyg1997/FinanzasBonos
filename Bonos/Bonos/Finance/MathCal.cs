﻿using Bonos.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bonos.Finance
{
    public class MathCal
    {

        public static double HallarCostesInicialesEmisor( double valorComercial, double pEstructuracion, double pColocacion, double pFlotacion, double pCAVALI)
        {
            double suma = pEstructuracion + pFlotacion + pColocacion + pCAVALI;
            return Math.Round(suma * valorComercial, 2);
        }

        public static double HallarCostesInicialesBonista(double valorComercial, double pEstructuracion, double pColocacion, double pFlotacion, double pCAVALI)
        {
            double suma = pFlotacion + pCAVALI;
            return Math.Round(suma * valorComercial, 2);
        }

        public static double HallarCuota(double bono, double TEP, int numeroCuotas)
        {
            return -Math.Round(bono * (Math.Pow(1 + TEP, numeroCuotas) * TEP) / (Math.Pow(1 + TEP, numeroCuotas) - 1), 2);
        }

        public static double HallarPrecioActual(List<Periodo> periodos, Calculo calculo)
        {
            double resultado = 0;
            for (int i = 1; i < periodos.Count; i++)
            {
                resultado = resultado + (periodos[i].flujoBonista.Value / Math.Pow(calculo.COK + 1, i));
            }
            return Math.Round(resultado, 2);
        }


        public static Calculo Resultados(Bono bono, List<Periodo> periodos)
        {
            double sumaFAP = 0;
            double sumaFA = 0;
            double sumaFC = 0;
            
            Calculo calculo = new Calculo();
            calculo.totalPeriodos = (bono.diasAño / bono.frecuencia) * bono.años;
            calculo.TEA = bono.tipoInteres == "Efectiva" ? bono.tasaInteres : Utils.HallarTEA(bono.tasaInteres, bono.diasAño, bono.capitalizacion.Value);
            calculo.TEP = Utils.HallarTEP(bono.tasaInteres, bono.diasAño, bono.frecuencia, bono.capitalizacion);
            calculo.COK = Utils.HallarCOK(bono.tasaDescuento, bono.diasAño, bono.frecuencia);
            calculo.costesInicialesEmisor = HallarCostesInicialesEmisor(bono.vcomercial, bono.pEstructura, bono.pColoca, bono.pFlota, bono.pCAVALI);
            calculo.costesInicialesBonista = HallarCostesInicialesBonista(bono.vcomercial, bono.pEstructura, bono.pColoca, bono.pFlota, bono.pCAVALI);
            if (periodos != null)
            {
                for (int i = 1; i < periodos.Count; i++)
                {
                    sumaFAP = sumaFAP + periodos[i].flujoActivoPlazo.Value;
                    sumaFA = sumaFA + periodos[i].flujoActivo.Value;
                    sumaFC = sumaFC + periodos[i].factorConvexidad.Value;
                }
                calculo.duracion = Math.Round(sumaFAP / sumaFA, 2);
                calculo.convexidad = Math.Round(sumaFC / (Math.Pow(1 + calculo.COK, 2) * sumaFA * Math.Pow(bono.diasAño / bono.frecuencia, 2)), 2);
                calculo.total = Math.Round(calculo.duracion + calculo.convexidad, 2);
                calculo.duracionModificada = Math.Round(calculo.duracion / (1 + calculo.COK), 2);
                calculo.precioActual = HallarPrecioActual(periodos, calculo);
                calculo.utilidad = Math.Round(periodos[0].flujoBonista.Value + HallarPrecioActual(periodos, calculo), 2);
                double[] flujosEmisor = periodos.Select(x => x.flujoEmisor.Value).ToArray();
                double[] flujosEmisorEscudo = periodos.Select(x => x.flujoEmisorEscudo.Value).ToArray();
                double[] flujosBonista = periodos.Select(x => x.flujoBonista.Value).ToArray();
                calculo.TCEA = Math.Round(Math.Pow(Financial.IRR(ref flujosEmisor) + 1, (double)bono.diasAño / bono.frecuencia) - 1, 9) * 100;
                calculo.TCEAEmisor = Math.Round(Math.Pow(Financial.IRR(ref flujosEmisorEscudo) + 1, (double)bono.diasAño / bono.frecuencia) - 1, 9) * 100;
                calculo.TREA = Math.Round(Math.Pow(Financial.IRR(ref flujosBonista) + 1, (double)bono.diasAño / bono.frecuencia) - 1, 9) * 100;
            }

            return calculo;
        }

        public static List<Periodo> ResultadosPeriodos(Bono bono, Calculo calculo, List<Periodo> periodos)
        {
            List<Periodo> lista = new List<Periodo>();
            double flujoEmisor = Math.Round(bono.vcomercial - calculo.costesInicialesEmisor, 7);
            double flujoBonista = Math.Round(-bono.vcomercial - calculo.costesInicialesBonista, 7);
            Periodo cero = new Periodo
            {
                N = 0,
                plazoGracia = null,
                bono = null,
                cupon = null,
                cuota = null,
                amortizacion = null,
                prima = null,
                escudo = null,
                flujoEmisor = flujoEmisor,
                flujoEmisorEscudo = flujoEmisor,
                flujoBonista = flujoBonista,
                flujoActivo = null,
                flujoActivoPlazo = null,
                factorConvexidad = null,
            };
            lista.Add(cero);
            for (int i = 1; i <= calculo.totalPeriodos; i++)
            {
                Periodo aux = new Periodo();
                aux.N = i;
                if (periodos.Count > 0) aux.plazoGracia = periodos[i].plazoGracia;
                aux.bono = i == 1 ? bono.vnominal : Math.Round(lista[i - 1].bono.Value + lista[i - 1].amortizacion.Value, 2);
                if (periodos.Count > 0 && periodos[i - 1].plazoGracia == "T" && i != 1) aux.bono = Math.Round(lista[i - 1].bono.Value - lista[i - 1].cupon.Value, 2);
                aux.cupon = Math.Round(-aux.bono.Value * calculo.TEP, 2);
                aux.cuota = HallarCuota(aux.bono.Value, calculo.TEP, calculo.totalPeriodos - aux.N + 1);
                if (periodos.Count > 0 && periodos[i].plazoGracia == "T") aux.cuota = 0;
                if (periodos.Count > 0 && periodos[i].plazoGracia == "P") aux.cuota = aux.cupon;
                aux.amortizacion = Math.Round(aux.cuota.Value - aux.cupon.Value, 2);
                if (periodos.Count > 0 && (periodos[i].plazoGracia == "T" || periodos[i].plazoGracia == "P")) aux.amortizacion = 0;
                aux.prima = aux.N == calculo.totalPeriodos ? -Math.Round(bono.pPrima * bono.vnominal, 2) : 0;
                aux.escudo = Math.Round(-aux.cupon.Value * bono.impuestoRenta, 2);
                aux.flujoEmisor = Math.Round(aux.cuota.Value + aux.prima.Value, 2);
                aux.flujoEmisorEscudo = aux.escudo + aux.flujoEmisor;
                aux.flujoBonista = -Math.Round(aux.cuota.Value + aux.prima.Value, 2);
                aux.flujoActivo = Math.Round(aux.flujoBonista.Value / Math.Pow(1 + calculo.COK, aux.N), 2);
                aux.flujoActivoPlazo = Math.Round(aux.flujoActivo.Value * aux.N * bono.frecuencia / bono.diasAño, 2);
                aux.factorConvexidad = Math.Round(aux.flujoActivo.Value * aux.N * (1 + aux.N), 2);
                lista.Add(aux);
            }
            return lista;
        }
    }
}