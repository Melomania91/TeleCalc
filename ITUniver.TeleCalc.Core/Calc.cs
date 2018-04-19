using ITUniver.TeleCalc.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace ITUniver.TeleCalc.ConCalc
{
    public class Calc
    {
        private IOperation[] operations { get; set; }

        public Calc()
        {
            var opers = new List<IOperation>();
            //get current assembly
            var assembly = Assembly.GetExecutingAssembly();

            // get all types in it
            var classes = assembly.GetTypes();

            classes.ToList().ForEach(c =>
            {
                var interfaces = c.GetInterfaces();

                if (interfaces.Any(i => i == typeof(IOperation)))
                {
                    var obj = System.Activator.CreateInstance(c) as IOperation;
                    if (obj != null)
                        opers.Add(obj);
                }
            });          

            operations = opers.ToArray();
        }

        public IOperation[] GetOperations()
        {
            return operations;
        }

        public string[] GetOperationsName()
        {
            return operations.Select(o => o.Name).ToArray();
        }

        public double Exec(string operName, double x, double y)
        {
            var operation = operations.FirstOrDefault(c => c.Name == operName);

            if (operation == null)
                return double.NaN;

            operation.Args = new double[] { x, y };
            return (double)operation.Result;
        }

        #region Old
       
        public double Sum(double x, double y)
        {           
            return Exec("sum", x, y);
        }

        public double Subtraction(double x, double y)
        {
            return Exec("sub", x, y);
        }

        public double? Division(double x, double y)
        {
            return Exec("div", x, y);
        }

        public double Multiplication(double x, double y)
        {
            return Exec("mult", x, y);
        }

        #endregion
    }
}

