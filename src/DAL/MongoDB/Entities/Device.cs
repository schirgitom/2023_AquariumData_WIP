using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MongoDB.Entities
{
    public class Device : Entity
    {
        public String Name { get; set; }

        public String Description { get; set; }

        public Boolean Active { get; set; }

        public String Aquarium { get; set; }

        [EnumDataType(typeof(DeviceType))]
        public DeviceType DeviceType { get; set; }
    }

    public enum DeviceType
    {
        Pump,
        Water
    }
}
