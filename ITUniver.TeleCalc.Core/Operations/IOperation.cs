using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCalc.Core.Operations
{
    public interface IOperation
    {
        string Name { get; }

        double[] Args { set; }

        string Error { get; }

        double? Result { get; }
    }
}
