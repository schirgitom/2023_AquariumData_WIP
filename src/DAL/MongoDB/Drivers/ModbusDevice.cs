using DAL.MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MongoDB.Drivers
{
    public class ModbusDevice : Device
    {
        public String Host { get; set; }
        public int Port { get; set; }
        public int SlaveID { get; set; }
    }
}
