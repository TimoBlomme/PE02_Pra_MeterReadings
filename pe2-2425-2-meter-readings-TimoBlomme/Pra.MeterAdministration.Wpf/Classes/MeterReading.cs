using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pra.MeterAdministration.Wpf.Classes
{
    public class MeterReading
    {
        public int MeterId { get; set; }
        public DateTime Date { get; set; }
        public MeterType MeterType { get; set; }
        public MeterReading(MeterType meterType, int meterId, DateTime date)
        {
            MeterType = meterType;
            MeterId = meterId;
            Date = date;
        }
        
    }
}
