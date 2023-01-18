using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Expenses_Tracker___Grupo_02
{
    public class CRUDs
    {
        public string create(List<string> x, string y)
        {
            x.Add(y);
            return y;
        }
        public string edit(List<string> x, string y, string z)
        {
            int index = x.IndexOf(y);
            x.RemoveAt(index);
            x.Insert(index, z);

            return x.ToString();
        }
        public void delete(List<string> x, string y)
        {
            x.Remove(y);
        }
        public void read(List<string> x)
        {
            foreach (string word in x)
            {
                Console.WriteLine(word);
            }
        }
    }
}
