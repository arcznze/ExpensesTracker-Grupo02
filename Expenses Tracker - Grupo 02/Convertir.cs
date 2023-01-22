using Expenses_Tracker___Grupo_02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Expenses_Tracker___Grupo_02
{
    public class Convertir
    {
        IBuscadorTasas buscadorTasas;
        //public Convertir(IBuscadorTasas buscadorTasas)
        //{
        //    this.buscadorTasas = buscadorTasas;
        //}
        //public float ComprarDolares(float dolares)
        //{
        //    var tasas = buscadorTasas.ObtenerTasas();
        //    var tasaVenta = tasas.Where(x => x.Entidad == "Banco BHD León"
        //                                             && x.MonedaOrigen == "DOP"
        //                                             && x.MonedaDestino == "USD").First();
        //    return dolares * tasaVenta.Valor;
        //}

        public Convertir(IBuscadorTasas buscadorTasas)
        {
            this.buscadorTasas = buscadorTasas;
        }

        //Opcional
        //public void MostrarReporteDeTasas()
        //{
        //    var tasas = buscadorTasas.ObtenerTasas();
        //    foreach(Tasa tasa in tasas.Result)
        //        Console.WriteLine($"{tasa.Entidad,-45}  {tasa.Valor,6}  {tasa.MonedaOrigen}->{tasa.MonedaDestino}");
        //}

        public async Task<float> DolaresAPesos (float pesos)
        {
            var tasas = await buscadorTasas.ObtenerTasas();
            var tasaVenta = tasas.Where(x => x.Entidad == "Banco BHD León"
                                        && x.MonedaOrigen == "USD"
                                        && x.MonedaDestino == "DOP").First();

            return (float)Math.Round((decimal)pesos * tasaVenta.Valor, 2);
        }


    }
}
