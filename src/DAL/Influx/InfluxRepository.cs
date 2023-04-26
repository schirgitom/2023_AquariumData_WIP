using DAL.Influx.Samples;
using DAL.MongoDB.Entities;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Writes;
using Serilog;
using System.Collections.Concurrent;
using Utils;

namespace DAL.Influx
{
    public class InfluxRepository : IInfluxRepository
    {

        protected ILogger log = Logger.ContextLog<InfluxRepository>();
        protected InfluxDBContext InfluxDBContext = null;
        String organisation;
        TimeSpan utcOffset;

        public InfluxRepository(InfluxDBContext context)
        {
            this.InfluxDBContext = context;
            organisation = context.Organisation;

            utcOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
        }

        private PointData GeneratePoint(Sample measurement)
        {


            if (measurement.GetType() == typeof(BinarySample))
            {
                var point = PointData.Measurement(measurement.Tag)
                .Tag("measurement", measurement.Tag)
                .Field("value", measurement.AsBoolean())
                .Timestamp(measurement.Timestamp.ToUniversalTime(), WritePrecision.S);
                return point;


            }
            else
            {
                var point = PointData.Measurement(measurement.Tag)
                .Tag("measurement", measurement.Tag)
                .Field("value", measurement.AsNumeric())
                .Timestamp(measurement.Timestamp.ToUniversalTime(), WritePrecision.S);
                return point;
            }

            return null;

        }


        public async Task InsertOneAsync(String bucket, Sample measurement)
        {
            var point = GeneratePoint(measurement);

            await InfluxDBContext.WriteAPI.WritePointAsync(point, bucket, InfluxDBContext.Organisation);
        }

        public async Task InsertManyAsync(String bucket, ConcurrentBag<Sample> measurement)
        {
            List<PointData> points = new List<PointData>();

            foreach (Sample meas in measurement)
            {
                points.Add(GeneratePoint(meas));
            }

            await InfluxDBContext.WriteAPI.WritePointsAsync(points, bucket, InfluxDBContext.Organisation);
        }


        private List<Sample> GetSamples(String bucket, DataPoint dp, List<FluxTable> tables)
        {
            List<Sample> returnval = new List<Sample>();
            if (dp.DataType == DataType.Boolean)
            {
                foreach (var record in tables.SelectMany(table => table.Records))
                {
                    BinarySample smp = new BinarySample();
                    smp.Timestamp = record.GetTime().Value.ToDateTimeUtc().ToLocalTime().AddHours(utcOffset.Hours);
                    smp.Value = Boolean.Parse(record.GetValue().ToString());
                    smp.Tag = record.GetMeasurement();

                    returnval.Add(smp);
                }
            }
            else
            {
                foreach (var record in tables.SelectMany(table => table.Records))
                {
                    NumericSample smp = new NumericSample();
                    smp.Timestamp = record.GetTime().Value.ToDateTimeUtc().ToLocalTime().AddHours(utcOffset.Hours);
                    smp.Value = float.Parse(record.GetValue().ToString());
                    smp.Tag = record.GetMeasurement();

                    returnval.Add(smp);
                }
            }

            return returnval;
        }

        public async Task<List<Sample>> GetInRange(String bucket, DataPoint dp, DateTime from, DateTime to)
        {

            long start = from.ToUnixTimeStamp();

            long end = to.ToUnixTimeStamp();


            var flux = "from(bucket:\"" + bucket + "\") |> range(start: " + start + ", stop: " + end + ") |> filter(fn: (r) => r[\"measurement\"] == \"" + dp.Name + "\")  ";
            var tables = await InfluxDBContext.QueryAPI.QueryAsync(flux, organisation);


            if (dp != null)
            {

                return GetSamples(bucket, dp, tables);


            }

            return null;

        }

        public async Task<Sample> GetLast(String bucket, DataPoint dp)
        {
            if (dp != null)
            {
                var flux = "from(bucket:\"" + bucket + "\")  |> range(start: -2h)   |> filter(fn: (r) => r[\"measurement\"] == \"" + dp.Name + "\") |> last() ";
                var tables = await InfluxDBContext.QueryAPI.QueryAsync(flux, organisation);


                if (dp != null)
                {
                    List<Sample> frmdb = GetSamples(bucket, dp, tables);

                    if (frmdb != null && frmdb.Count > 0)
                    {
                        return frmdb[0];
                    }
                }
            }

            return null;
        }

        public async Task CreateBucket(string bucket)
        {
            if (!String.IsNullOrEmpty(bucket))
            {
                Bucket ininflux = await InfluxDBContext.BucketsAPI.FindBucketByNameAsync(bucket);

                if (ininflux == null)
                {
                    await InfluxDBContext.BucketsAPI.CreateBucketAsync(bucket, organisation);
                }
            }
        }
    }
}