using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCalc.Core.Operations
{
    internal class Sum : IOperation
    {
        public string Name => "sum";

        public double[] Args { set {
                var sum = 0d;
                value.ToList().ForEach(v => 
                {                   
                    sum += v;
                });
                
                Result = sum;
            }
        get { return new double[0]; }
                }

        public string Error { get; }

        public double? Result { get; private set; }
    }


}
