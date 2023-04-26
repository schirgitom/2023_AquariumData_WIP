using Context.DataBaseSettings;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DAL.Influx
{
    public class InfluxUnitOfWork
    {
        protected ILogger log = Logger.ContextLog<InfluxUnitOfWork>();

        public InfluxDBContext Context { get; private set; } = null;

        private IInfluxRepository Repository = null;

        public InfluxUnitOfWork()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Constants.CurrentFolder).AddJsonFile("appsettings.json");

            InfluxDBSettings settings = builder.Build().GetSection("InfluxDbSettings").Get<InfluxDBSettings>();
            InfluxDBContext context = new InfluxDBContext(settings);
            Context = context;

            Repository = new InfluxRepository(Context);
        }

        public IInfluxRepository Influx
        {
            get
            {
                return Repository;
            }
        }
    }
}
