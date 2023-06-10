using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeEx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now;
            Console.WriteLine($".Now : {dt}");
            Console.WriteLine($".ToShortDateString : {dt.ToShortDateString()}");
            Console.WriteLine($".ToShortTimeString : {dt.ToShortTimeString()}");
            Console.WriteLine($" String.Format : {String.Format("{0:dd/MM/yyyy}", dt)}");
            var d = Console.ReadLine();
            Console.ReadKey();
        }
    }
}
