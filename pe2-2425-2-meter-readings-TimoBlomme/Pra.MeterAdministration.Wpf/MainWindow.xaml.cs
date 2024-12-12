﻿using Pra.MeterAdministration.Wpf.Classes;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Pra.MeterAdministration.Wpf
{
    public partial class MainWindow : Window
    {
        private MeterAdmin meterAdmin = new MeterAdmin();

        public MainWindow()
        {
            InitializeComponent();
            InitializeMeterTypeComboBox();
        }

        private void InitializeMeterTypeComboBox()
        {
            cmbMeterType.ItemsSource = Enum.GetValues(typeof(MeterType));
        }

        private void CmbMeterType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stpControls.Children.Clear();

            if (cmbMeterType.SelectedItem is MeterType selectedType)
            {
                switch (selectedType)
                {
                    case MeterType.AirQuality:
                        AddControlToUI("CO2 (ppm):", "txtCO2");
                        AddControlToUI("PM2.5 (µg/m3):", "txtPM25");
                        break;
                    case MeterType.SolarPanel:
                        AddControlToUI("Energy Production (kWh):", "txtEnergyProduction");
                        break;
                    case MeterType.Electricity:
                        AddControlToUI("Energy Consumption (kWh):", "txtEnergyConsumption");
                        AddComboBoxToUI("Type (PEAKLOAD/OFFPEAKLOAD):", "cmbConsumptionType");
                        break;
                    case MeterType.Water:
                        AddControlToUI("Water Consumption (liters):", "txtWaterConsumption");
                        break;
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int meterId;
                if (!int.TryParse(txtMeterId.Text, out meterId))
                {
                    throw new Exception("Invalid meter ID.");
                }

                if (!dteDate.SelectedDate.HasValue)
                {
                    throw new Exception("Select a valid date.");
                }

                Dictionary<string, string> values = new Dictionary<string, string>();
                foreach (TextBox child in stpControls.Children.OfType<TextBox>())
                {
                    values[child.Name] = child.Text;
                    if (string.IsNullOrWhiteSpace(child.Text))
                    {
                        throw new Exception($"Invalid input for {child.Name}.");
                    }
                }

                foreach (MeterReading existingReading in meterAdmin.MeterReadings)
                {
                    if (existingReading.MeterId == meterId)
                    {
                        throw new Exception("A meter reading with this ID already exists.");
                    }
                }

                foreach (ComboBox comboBox in stpControls.Children.OfType<ComboBox>())
                {
                    values[comboBox.Name] = comboBox.SelectedItem?.ToString();
                    if (comboBox.SelectedItem == null)
                    {
                        throw new Exception($"Please select a valid option for {comboBox.Name}.");
                    }
                }

                MeterReading reading = new MeterReading
                {
                    MeterId = meterId,
                    Date = dteDate.SelectedDate.Value,
                    MeterType = cmbMeterType.SelectedItem.ToString(),
                    Values = values
                };

                meterAdmin.AddMeterReading(reading);
                RefreshMeterReadingsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshMeterReadingsList()
        {
            lstMeterReadings.Items.Clear();

            foreach (MeterReading reading in meterAdmin.MeterReadings)
            {
                string displayText = $"{reading.MeterType} Meter ID: {reading.MeterId}, Date: {reading.Date.ToShortDateString()} : ";

                switch (reading.MeterType)
                {
                    case "AirQuality":
                        if (reading.Values.ContainsKey("txtCO2") && reading.Values.ContainsKey("txtPM25"))
                        {
                            displayText += $"CO2: {reading.Values["txtCO2"]} ppm, PM2.5: {reading.Values["txtPM25"]} µg/m³";
                        }
                        break;
                    case "SolarPanel":
                        if (reading.Values.ContainsKey("txtEnergyProduction"))
                        {
                            displayText += $"Energy Production: {reading.Values["txtEnergyProduction"]} kWh";
                        }
                        break;
                    case "Electricity":
                        if (reading.Values.ContainsKey("txtEnergyConsumption") && reading.Values.ContainsKey("cmbConsumptionType"))
                        {
                            displayText += $"{reading.Values["cmbConsumptionType"]} Usage: {reading.Values["txtEnergyConsumption"]} kWh";
                        }
                        break;
                    case "Water":
                        if (reading.Values.ContainsKey("txtWaterConsumption"))
                        {
                            displayText += $"Water Usage: {reading.Values["txtWaterConsumption"]} liters";
                        }
                        break;
                }
                lstMeterReadings.Items.Add(displayText);
            }
        }

        private void AddControlToUI(string labelText, string textBoxName)
        {
            TextBlock textBlock = new TextBlock { Text = labelText, Margin = new Thickness(0, 0, 10, 0) };
            TextBox textBox = new TextBox { Name = textBoxName };
            stpControls.Children.Add(textBlock);
            stpControls.Children.Add(textBox);
        }

        private void AddComboBoxToUI(string labelText, string comboBoxName)
        {
            TextBlock textBlock = new TextBlock { Text = labelText, Margin = new Thickness(0, 0, 10, 0) };
            ComboBox comboBox = new ComboBox
            {
                Name = comboBoxName,
                ItemsSource = new[] { "Peakload", "Offload" }
            };
            stpControls.Children.Add(textBlock);
            stpControls.Children.Add(comboBox);
        }
    }
}