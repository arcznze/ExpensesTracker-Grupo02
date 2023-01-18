using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Expenses_Tracker___Grupo_02
{
    public class Accounts
    {
        public string createAccount(List<string> x, string y)
        {
            x.Add(y);
            return y;
        }
        public string editAccount(List<string> x, string y, string z)
        {
            int index = x.IndexOf(y);
            x.RemoveAt(index);
            x.Insert(index, z);

            return x.ToString();
        }
        public void deleteAccount(List<string> x, string y)
        {
            x.Remove(y);
        }
        public void readAccount(List<string> x)
        {
            foreach (string word in x)
            {
                Console.WriteLine(word);
            }
        }
    }
}