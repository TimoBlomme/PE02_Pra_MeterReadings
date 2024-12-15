using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pra.MeterAdministration.Wpf.Classes
{
    internal class AirQuality : MeterReading
    {
        public int CO2 { get; set; } = 0;
        public int PM25 { get; set; } = 0;
        public AirQuality(int MeterId, MeterType meterType, DateTime date, int Co2, int Pm25) : base(meterType, MeterId, date)
        {
            CO2 = Co2;
            PM25 = Pm25;
        }
    }
}
