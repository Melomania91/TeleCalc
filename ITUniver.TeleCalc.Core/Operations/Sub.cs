using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCalc.Core.Operations
{
    internal class Sub : IOperation
    {
        public string Name => "sub";

        public double[] Args { set {
                var sub = value[0];
                value.Skip(1).ToList().ForEach(v =>
                {
                    sub -= v;
                });
                Result = sub;
            }
        get { return new double[0]; }
                }

        public string Error { get; }

        public double? Result { get; private set; }
    }


}
