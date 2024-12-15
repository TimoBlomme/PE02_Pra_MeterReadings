using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pra.MeterAdministration.Wpf.Classes
{
    public enum MeterType
    {
        [Display(Name = "Water Meter")]
        Water,
        [Display(Name = "Electricity Meter")]
        Electricity,
        [Display(Name = "Solar Panel")]
        SolarPanel,
        [Display(Name = "Air Quality Meter")]
        AirQuality
        /*
        AirQuality,
        SolarPanel,
        Electricity,
        Water
        */
    }
}
