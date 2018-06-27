using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bonos.Helpers
{
    public class Selects
    {
        public static List<SelectListItem> frecuenciaBono { get; set; }
        public static List<SelectListItem> diasAño { get; set; }
        public static List<SelectListItem> tipoInteres { get; set; }
        public static void llenarDatos()
        {
            frecuenciaBono = new List<SelectListItem>();
            diasAño = new List<SelectListItem>();
            tipoInteres = new List<SelectListItem>();

            frecuenciaBono.Add(new SelectListItem
            {
                Disabled = true,
                Selected = true,
                Text = "-- Frecuencia del bono --"
                
            });
            frecuenciaBono.Add(new SelectListItem
            {
                Text = "Mensual",
                Value = "30"
            });
            frecuenciaBono.Add(new SelectListItem
            {
                Text = "Bimestral",
                Value = "60",
            });
            frecuenciaBono.Add(new SelectListItem
            {
                Text = "Trimestral",
                Value = "90",
            });
            frecuenciaBono.Add(new SelectListItem
            {
                Text = "Cuatrimestral",
                Value = "120",
            });
            frecuenciaBono.Add(new SelectListItem
            {
                Text = "Semestral",
                Value = "180",
            });
            frecuenciaBono.Add(new SelectListItem
            {
                Text = "Anual",
                Value = "360",
            });

            diasAño.Add(new SelectListItem
            {
                Disabled = true,
                Selected = true,
                Text = "-- Tipo de Año --"

            });
            diasAño.Add(new SelectListItem
            {
                Text = "360",
                Value = "360"
            });
            diasAño.Add(new SelectListItem
            {
                Text = "365",
                Value = "365"
            });

            tipoInteres.Add(new SelectListItem
            {
                Disabled = true,
                Selected = true,
                Text = "-- Tipo Interes --"

            });

            tipoInteres.Add(new SelectListItem
            {
                Text = "Nominal",
                Value = "Nominal"
            });
            tipoInteres.Add(new SelectListItem
            {
                Text = "Efectiva",
                Value = "Efectiva"
            });
        }
    }
}