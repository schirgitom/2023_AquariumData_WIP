namespace DAL.MongoDB.Entities.Devices
{
    public class MQTTDevice : Device
    {
        public string Host { get; set; }
        public int Port { get; set; }

    }
}
