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
            if (meterReadings.Count > 4)
            {
                MessageBox.Show("you already have 5 Readings, you cannot add more readings", "Max amount of readings", MessageBoxButton.OK);
                return;
            }
            meterReadings.Add(reading);
        }

        public void RemoveMeterReading(MeterReading reading)
        {
            meterReadings.Remove(reading);
        }

        public void UpdateMeterReading(int meterId, Dictionary<string, string> updatedValues, DateTime updatedDate)
        {
            MeterReading reading = meterReadings.FirstOrDefault(r => r.MeterId == meterId);
            if (reading != null)
            {
                reading.Values = updatedValues;
                reading.Date = updatedDate;
            }
            else
            {
                throw new KeyNotFoundException($"No meter reading found with ID: {meterId}");
            }
        }

        public List<MeterReading> GetReadingsByType(string meterType)
        {
            return meterReadings.Where(r => r.MeterType == meterType).ToList();
        }


    }
}

