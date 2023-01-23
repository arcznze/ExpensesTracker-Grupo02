using Expenses_Tracker___Grupo_02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses_Tracker___Grupo_02;

public class Convertir
{
    IBuscadorTasas buscadorTasas;
    public Convertir(IBuscadorTasas buscadorTasas)
    {
        this.buscadorTasas = buscadorTasas;
    }
    public float DolaresAPesos(float dolares)
    {
        var tasas = buscadorTasas.ObtenerTasas().GetAwaiter().GetResult();
        var tasaVenta = tasas.Where(x => x.Entidad == "Banco BHD León"
                                    && x.MonedaOrigen == "USD"
                                    && x.MonedaDestino == "DOP").FirstOrDefault();

        return dolares * tasaVenta.Valor;
    }
}
}
