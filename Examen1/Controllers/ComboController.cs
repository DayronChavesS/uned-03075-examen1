using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen1.Models;

namespace Examen1.Controllers
{
    public class ComboController : Controller
    {
        public static List<Combo> CombosEncargados = null;
        public static int subtotal_dolares;
        public static int subtotal_colones;
        public static int monto_de_descuento;
        public static int monto_de_IVA;
        public static int monto_total;
        public static int monto_total_cancelar;

        public ComboController()
        {
            subtotal_dolares = 0;
            subtotal_colones = 0;
            monto_de_descuento = 0;
            monto_de_IVA = 0;
            monto_total = 0;
            monto_total_cancelar = 0;

            if (CombosEncargados == null)
            {
                CombosEncargados = new List<Combo>();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult CrearCombo1(Combo combocreado)
        {
            combocreado.ComboElegido = 1;

            for (int i = 0; i < combocreado.CantidadCombos; i++)
            {
                CombosEncargados.Add(combocreado);
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult CrearCombo2(Combo combocreado)
        {
            combocreado.ComboElegido = 2;
            for (int i = 0; i < combocreado.CantidadCombos; i++)
            {
                CombosEncargados.Add(combocreado);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Facturar()
        {
            ViewBag.CantCombo1 = CalcularCantidadCombo1();
            ViewBag.CantCombo2 = CalcularCantidadCombo2();
            ViewBag.Subtotal = CalcularSubTotal();
            ViewBag.Monto_Descuento = CalcularDescuento();
            ViewBag.Monto_IVA = CalcularMontoConIVA();
            ViewBag.Monto_Total = CalcularMontoTotal();
            ViewBag.Monto_Cancelar = CalcularMontoTotalCancelar();
            return View();
        }

        public int CalcularCantidadCombo1()
        {
            int cantidad = 0;

            foreach (Models.Combo Combo in CombosEncargados)
            {
                if (Combo.ComboElegido == 1)
                {
                    cantidad++;
                }
            }

            return cantidad;
        }

        public int CalcularCantidadCombo2()
        {
            int cantidad = 0;

            foreach (Models.Combo Combo in CombosEncargados)
            {
                if (Combo.ComboElegido == 2)
                {
                    cantidad++;
                }
            }

            return cantidad;
        }

        public int CalcularSubTotal()
        {


            foreach (Models.Combo Combo in CombosEncargados)
            {
                if (Combo.ComboElegido == 1 && Combo.Horario1 == true)
                {
                    subtotal_dolares = subtotal_dolares + 600;
                }
                else if (Combo.ComboElegido == 1 && Combo.Horario2 == true)
                {
                    subtotal_dolares = subtotal_dolares + 700;
                }
                else if (Combo.ComboElegido == 2 && Combo.Horario1 == true)
                {
                    subtotal_dolares = subtotal_dolares + 700;
                }
                else if (Combo.ComboElegido == 2 && Combo.Horario2 == true)
                {
                    subtotal_dolares = subtotal_dolares + 850;
                }
            }

            subtotal_colones = subtotal_dolares * 623;

            return subtotal_colones;
        }

        public int CalcularDescuento()
        {
            if (CombosEncargados.Count() >= 3)
            {
                monto_de_descuento = (subtotal_colones / 100) * 5;
            }

            return monto_de_descuento;
        }
        public int CalcularMontoConIVA()
        {
            monto_de_IVA = (subtotal_colones / 100) * 13;

            return monto_de_IVA;
        }
        public int CalcularMontoTotal()
        {
            monto_total = subtotal_colones + monto_de_IVA;
            return monto_total;
        }
        public int CalcularMontoTotalCancelar()
        {
            monto_total_cancelar = monto_total - monto_de_descuento;
            return monto_total_cancelar;
        }
    }
}
