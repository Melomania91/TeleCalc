using ITUniver.TeleCalc.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCalc.ConCalc
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var calc = new Calc();
            var operations = calc.GetOperations();

            string operationName;
            double x = 0;
            double y = 0;
            double result = 0;

            // не указаны аргументы
            if (args.Length != 3)
            {
                Console.WriteLine(string.Format("Пожалуйста, укажите операцию." +
                "\nДоступные операции: {0}", string.Join(", ", operations.Select(o => o.Name))));

                operationName = GetOperationName(operations);
                x = GetNumber();
                y = GetNumber();

            }

            // указаны аргументы
            else
            {
                operationName = args[0];
                if (!operations.Any(o => o.Name == operationName))
                {
                    operationName = GetOperationName(operations);
                }

                if (!(Double.TryParse(args[1], out x) && Double.TryParse(args[2], out y)))
                {
                    x = GetNumber();
                    y = GetNumber();
                }                   
            }

            result = calc.Exec(operationName, x, y);
            Console.WriteLine(string.Format("{0} {1} {2} = {3}", x, operationName, y, result));
            Console.ReadKey();
        }

        static string GetOperationName(IOperation[] operations)
        {
            var oper = Console.ReadLine();
            if (!operations.Any(o => o.Name == oper))
            {
                Console.WriteLine(string.Format("Операция не найдена." +
            "\nДоступные операции: {0}", string.Join(", ", operations.Select(o => o.Name))));
                return GetOperationName(operations);
            }
            else
            {
                return oper;
            }
        }

        static double GetNumber()
        {
            Console.WriteLine("Введите число");
            var num = Console.ReadLine();
            double x;
            if (!Double.TryParse(num, out x))
            {
                Console.WriteLine("Ошибка ввода. Введите число");
                return GetNumber();
            }
            else
            {
                return x;
            }
        }
    }

}
