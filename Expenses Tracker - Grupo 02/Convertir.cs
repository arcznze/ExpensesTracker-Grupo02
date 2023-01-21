using Expenses_Tracker___Grupo_02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses_Tracker___Grupo_02
{
    public class Convertir
    {
        IBuscadorTasas buscadorTasas;
        public Convertir(IBuscadorTasas buscadorTasas)
        {
            this.buscadorTasas = buscadorTasas;
        }
        public float ComprarDolares(float dolares)
        {
            var tasas = buscadorTasas.ObtenerTasas();
            var tasaVenta = tasas.Where(x => x.Entidad == "Banco BHD León"
                                                     && x.MonedaOrigen == "DOP"
                                                     && x.MonedaDestino == "USD").First();
            return dolares * tasaVenta.Valor;
        }
    }
}
