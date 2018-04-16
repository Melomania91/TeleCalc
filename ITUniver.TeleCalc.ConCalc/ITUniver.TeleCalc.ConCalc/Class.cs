using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCalc.ConCalc
{
    public class Calc
    {
        public double Sum(double x, double y)
        {
            return x + y;
        }

        public double Subtraction(double x, double y)
        {
            return x - y;
        }
       
        public double? Division(double x, double y)
        {
            if (y != 0)
                return x / y;
            else
                return null;
        }

        public double Multiplication(double x, double y)
        {
            return x * y;
        }
    }
}
