using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Expenses_Tracker___Grupo_02
{
    public class accounts
    {
        List<string> listAux = new List<string>();
        public string crearCategoria(string x)
        {
            listAux.Add(x);
            return x;
        }
        public string editarCategoria(string x, string y)
        {
            int index = listAux.IndexOf(x);
            listAux.RemoveAt(index);
            listAux.Insert(index, y);

            return "";
        }
        public void eliminarCategoria(List<string> x, string y)
        {
            x.Remove(y);
        }
        public void verCategorias()
        {
            foreach (string word in listAux)
            {
                Console.WriteLine(word);
            }
        }
    }
}
