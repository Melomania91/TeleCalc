using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCalc.ConCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {

                Console.WriteLine("Пожалуйста, введите операцию и два числа");
                return;
            }
            double x = Double.Parse(args[1]);
            double y = Double.Parse(args[1]);

            var calc = new Calc();
            double? result = 0;
            switch (args[0])
            {
                case "+":
                    result = calc.Sum(x, y);
                    break;
                case "-":
                    result = calc.Subtraction(x, y);
                    break;
                case "/":
                    result = calc.Division(x, y);
                    break;
                case "*":
                    result = calc.Multiplication(x, y);
                    break;
            }

            Console.WriteLine(string.Format("{0}{1}{2} = {3}", x, args[0], y, result));
            Console.ReadKey();
        }
    }
}
