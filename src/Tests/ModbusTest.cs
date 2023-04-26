using DAL.MongoDB.Drivers;
using Services.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ModbusTest
    {
        [Test]
        public async Task ReadModbus()
        {
            ModbusDevice modbusDevice = new ModbusDevice();
            modbusDevice.Host = "127.0.0.1";
            modbusDevice.Port = 502;
            modbusDevice.SlaveID = 1;
            modbusDevice.Name = "Test";
            modbusDevice.DeviceType = DAL.MongoDB.Entities.DeviceType.Pump;
            modbusDevice.Active = true;

            List<ModbusDataPoint> dataPoints = new List<ModbusDataPoint>();

            ModbusDataPoint current = new ModbusDataPoint();
            current.DataType = DAL.MongoDB.Entities.DataType.Float;
            current.Register = 0;
            current.RegisterCount = 2;
            current.ReadingType = ReadingType.LowToHigh;
            current.RegisterType = RegisterType.HoldingRegister;
            current.Name = "Current";

            dataPoints.Add(current);


            ModbusDriver driver = new ModbusDriver(modbusDevice, dataPoints);
            await driver.Connect();
            Assert.IsTrue(driver.IsConnected);
            await driver.Read();
            Assert.Greater(driver.Measurements.Count, 0);
            await driver.Disconnect();


        }
    }
}
