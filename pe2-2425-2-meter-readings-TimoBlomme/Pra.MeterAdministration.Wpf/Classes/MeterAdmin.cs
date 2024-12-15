using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Pra.MeterAdministration.Wpf.Classes
{
    public class MeterAdmin
    {
        private List<MeterReading> meterReadings;

        public MeterAdmin()
        {
            meterReadings = new List<MeterReading>();
        }

        public IReadOnlyList<MeterReading> MeterReadings
        {
            get { return meterReadings.AsReadOnly(); }
        }

        public int GetMetersCount()
        {
            return meterReadings.Count;
        }

        public void AddMeterReading(MeterReading reading)
        {
            meterReadings.Add(reading);
        }

        public void RemoveMeterReading(MeterReading reading)
        {
            meterReadings.Remove(reading);
        }

        public void UpdateMeterReading(int meterId, int value, DateTime updatedDate, int value2 = 0, ConsumptionType consumptionType = ConsumptionType.OFFPEAKLOAD)
        {

            Water waterReading;
            Solar solarReading;
            Electricity electricityReading;
            AirQuality airQualityReading;

            MeterReading reading = meterReadings.FirstOrDefault(r => r.MeterId == meterId);
            

            if (reading != null)
            {
                switch (reading.MeterType)
                {
                    case MeterType.Water:
                        waterReading = (Water)reading;
                        waterReading.Liters = value;
                        break;
                    case MeterType.Electricity:
                        electricityReading = (Electricity)reading;
                        electricityReading.Energy = value;
                        electricityReading.ConsumptionType = consumptionType;
                        break;
                    case MeterType.SolarPanel:
                        solarReading = (Solar)reading;
                        solarReading.Energy = value;
                        break;
                    case MeterType.AirQuality:
                        airQualityReading = (AirQuality)reading;
                        airQualityReading.CO2 = value;
                        airQualityReading.PM25 = value2;
                        break;
                }
                reading.Date = updatedDate;
            }
            else
            {
                throw new KeyNotFoundException($"No meter reading found with ID: {meterId}");
            }
        }

        public List<MeterReading> GetReadingsByType(string meterType)
        {
            return meterReadings.Where(r => r.MeterType.ToString() == meterType).ToList();
        }


    }
}