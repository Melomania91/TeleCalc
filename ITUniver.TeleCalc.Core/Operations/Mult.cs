using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCalc.Core.Operations
{
    internal class Mult : IOperation
    {
        public string Name => "mult";

        public double[] Args { set {
                var mult = 1d;
                value.ToList().ForEach(v =>
                {
                    mult *= v;
                });
                Result = mult;
            }
        get { return new double[0]; }
                }

        public string Error { get; }

        public double? Result { get; private set; }
    }


}
