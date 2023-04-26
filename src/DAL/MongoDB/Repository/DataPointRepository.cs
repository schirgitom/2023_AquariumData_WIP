using DAL.MongoDB.Entities;
using DAL.MongoDB.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MongoDB.Repository
{
    public class DataPointRepository : Repository<DataPoint>, IDataPointRepository
    {
        public DataPointRepository(DBContext Context) : base(Context)
        {
        }

        public List<DataPoint> GetDataPointsForDevice(DeviceType deviceType)
        {
            return FilterBy(x => x.DeviceName.Equals(deviceType.ToString()));
        }
    }
}
