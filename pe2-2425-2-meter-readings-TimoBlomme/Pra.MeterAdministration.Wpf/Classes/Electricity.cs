using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pra.MeterAdministration.Wpf.Classes
{
    internal class Electricity : MeterReading
    {
        public int Energy { get; set; } = 0;
        public ConsumptionType ConsumptionType { get; set; } = ConsumptionType.PEAKLOAD;
        public Electricity(int MeterId, MeterType meterType, DateTime date, int energy, ConsumptionType consumptionType) : base(meterType, MeterId, date)
        {
            Energy = energy;
            ConsumptionType = consumptionType;
        }
    }
}
