using DAL.MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MongoDB.Drivers
{
    public class ModbusDataPoint : DataPoint
    {
        [EnumDataType(typeof(RegisterType))]
        public RegisterType RegisterType { get; set; }

        [EnumDataType(typeof(ReadingType))]
        public ReadingType ReadingType { get; set; }

        public int Register { get; set; }

        public int RegisterCount { get; set; }
    }

    public enum RegisterType
    {
        InputRegister,
        HoldingRegister,
        Coil,
        InputStatus
    }

    public enum ReadingType
    {
        LowToHigh,
        HighToLow,
    }
}
