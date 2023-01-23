using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;

/*
 * Este archivo completo lo pueden copiar y pegar en su proyecto.
 * Para esto, tienen que tener instalado "AngleSharp" en su proyecto, y asegurense
 * de cambiar el nombre del namespace al que están usando.
 */

namespace Expenses_Tracker___Grupo_02
{
    // esto es para agrupar los valores de cada tasa
    public struct Tasa
    {
        public string Entidad;
        public float Valor;
        public string MonedaOrigen;
        public string MonedaDestino;

        public Tasa(float valor, string monedaOrigen, string monedaDestino, string entidad = "")
        {
            Entidad = entidad;
            Valor = valor;
            MonedaOrigen = monedaOrigen;
            MonedaDestino = monedaDestino;
        }

    }

    // Esta interfaz es para poder hacer un stub de BuscadorTasas para las Pruebas Unitarias
    public interface IBuscadorTasas
    {
        public Task<List<Tasa>> ObtenerTasas();
    }

    /* Esta es la clase que hace un request a infodolar.com.do y hace un scrapping de las tasas.
    Tendrán que hacer un stub de esta clase en el proyecto de Pruebas Unitarias, para sustituirla
    por una versión que no haga el request realmente (al final, descarté lo que les había comentado
    de hacer un stub del HttpClient o del BrowsingContext porque se complicaría demasiado).*/
    public class BuscadorTasas : IBuscadorTasas
    {
        public async Task<List<Tasa>> ObtenerTasas()
        {
            List<Tasa> tasas = new List<Tasa>();
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://www.infodolar.com.do/";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);
            var cells = document.QuerySelectorAll("table#Dolar tbody tr");

            foreach (var i in cells)
            {
                var bankName = i.Children[0].QuerySelector("span.nombre")?.TextContent.Trim() ?? "";
                var buyPriceConSimbolo = i.Children[1].TextContent.Split('\n')[1].Trim();
                var sellPriceConSimbolo = i.Children[2].TextContent.Split('\n')[1].Trim();
                float buyPrice = buyPriceConSimbolo != "" ? Convert.ToSingle(buyPriceConSimbolo.Replace("$", "")) : 0.0f;
                float sellPrice = sellPriceConSimbolo != "" ? Convert.ToSingle(sellPriceConSimbolo.Replace("$", "")) : 0.0f;
                tasas.Add(new Tasa(buyPrice, "USD", "DOP", bankName));
                tasas.Add(new Tasa(sellPrice, "DOP", "USD", bankName));
            }
            return tasas;

        }
    }
}