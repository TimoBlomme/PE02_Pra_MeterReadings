using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pra.MeterAdministration.Wpf.Classes
{
    internal class Water : MeterReading
    {
        public int Liters { get; set; } = 0;
        public Water(int MeterId, MeterType meterType, DateTime date, int liters) : base(meterType, MeterId, date)
        {
            Liters = liters;
        }
    }
}
