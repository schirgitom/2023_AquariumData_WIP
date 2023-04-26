using DAL.Influx;
using DAL.Influx.Samples;
using MongoDB.Driver.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class InfluxDBTests
    {
        InfluxUnitOfWork UnitOfWork = null;

        public InfluxDBTests()
        {
            UnitOfWork = new InfluxUnitOfWork();
        }

        [Test]
        public async Task CreateFirstEntry()
        {
            NumericSample sample = new NumericSample();
            sample.Tag = "Test";
            sample.Value = 7214;
            sample.Timestamp = DateTime.Now;

            await UnitOfWork.Influx.InsertOneAsync("Test", sample);

        }
    }
}
