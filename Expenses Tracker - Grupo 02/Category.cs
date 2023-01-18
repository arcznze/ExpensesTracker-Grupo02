using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses_Tracker___Grupo_02
{
    public class Category
    {
        List<string> listaCategorias = new List<string>();

        public string crearCategoria(string x)
        {
            listaCategorias.Add(x);
            return x;
        }
        public string editarCategoria(string x, string y)
        {
            int index = listaCategorias.IndexOf(x);
            listaCategorias.RemoveAt(index);
            listaCategorias.Insert(index, y);

            return "";
        }
        public string eliminarCategoria(string x)
        {
            listaCategorias.Remove(x);
            return "";
        }
        public void verCategorias()
        {
            foreach (string word in listaCategorias)
            {
                Console.WriteLine(word);
            }
        }
    }
}
