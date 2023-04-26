using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Influx.Samples
{
    public abstract class Sample
    {
        public Sample()
        {
            Timestamp = DateTime.Now; 
        }

        public String Tag { get; set; }
        public DateTime Timestamp { get; set; }
        public Object Value { get; set; }

        public abstract Boolean AsBoolean();

        public abstract Double AsNumeric();
    }
}
