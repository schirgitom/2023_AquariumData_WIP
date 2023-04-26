using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MongoDB.Entities
{
    public class DataPoint : Entity
    {
        public String Name { get; set; }

        public String Description { get; set; }
        public String DeviceName { get; set; }

        public int Offset { get; set; } = 1;

        [EnumDataType(typeof(DataType))]
        public DataType DataType { get; set; }

        public String DataPointVisual { get; set; }

    }

    public enum DataType
    {
        Boolean,
        Float,
        Integer
    }
}
