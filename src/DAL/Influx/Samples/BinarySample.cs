using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Influx.Samples
{
    public class BinarySample : Sample
    {
        public override bool AsBoolean()
        {
            return Convert.ToBoolean(Value);
        }

        public override double AsNumeric()
        {
            return Convert.ToDouble(Value);
        }
    }
}
