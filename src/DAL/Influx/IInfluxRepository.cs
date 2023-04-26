using DAL.Influx.Samples;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Influx
{
    public interface IInfluxRepository
    {
            Task InsertOneAsync(String bucket, Sample measurement);
            Task InsertManyAsync(String bucket, ConcurrentBag<Sample> measurement);
            Task<List<Sample>> GetInRange(String bucket, DataPoint dp, DateTime from, DateTime to);
            Task<Sample> GetLast(String bucket, DataPoint dp);
            Task CreateBucket(String bucket);
    }
}
