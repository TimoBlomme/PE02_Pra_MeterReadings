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
        public string MeterType { get; set; }
        public Dictionary<string, string> Values { get; set; }
    }
}
