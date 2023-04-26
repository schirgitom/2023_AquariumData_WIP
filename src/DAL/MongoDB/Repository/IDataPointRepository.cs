using DAL.MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MongoDB.Repository
{
    public interface IDataPointRepository : IRepository<DataPoint>
    {
        List<DataPoint> GetDataPointsForDevice(DeviceType deviceType);
    }
}
