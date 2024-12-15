using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pra.MeterAdministration.Wpf.Classes
{
    internal class Solar : MeterReading
    {
        public int Energy { get; set; } = 0;
        public Solar(int MeterId, MeterType meterType, DateTime date, int energy) : base(meterType, MeterId, date)
        {
            Energy = energy;
        }
    }
}
