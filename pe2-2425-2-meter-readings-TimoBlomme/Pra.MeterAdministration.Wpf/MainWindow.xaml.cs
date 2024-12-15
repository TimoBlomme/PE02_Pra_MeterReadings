using Pra.MeterAdministration.Wpf.Classes;
using System;
using System.Diagnostics.Metrics;
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
        private MeterReading selectedReading = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeMeterTypeComboBox();
            InitializeSeedData();
            InitializeUI();
            lstMeterReadings.SelectionChanged += LstMeterReadings_SelectionChanged;
        }

        private void InitializeMeterTypeComboBox()
        {
            cmbMeterType.ItemsSource = Enum.GetValues(typeof(MeterType));
        }

        private void InitializeUI()
        {
            cmbMeterType.ItemsSource = Enum.GetValues(typeof(MeterType)).Cast<MeterType>().ToList();
        }

        private void InitializeSeedData()
        {


            MeterReading airQualityReading = new AirQuality
            (
                1, MeterType.AirQuality, DateTime.Now, 20, 20
            );

            MeterReading waterReading = new Water
            (
                2, MeterType.Water, DateTime.Now, 200
            );

            meterAdmin.AddMeterReading(airQualityReading);
            meterAdmin.AddMeterReading(waterReading);

            RefreshMeterReadingsList();
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
                if (meterAdmin.GetMetersCount() >= 5)
                {
                    throw new Exception("Maximum of 5 meter readings reached.");
                }

                if (cmbMeterType.SelectedItem == null)
                {
                    throw new Exception("you have not selected a meterType");
                }

                MeterType meterType = (MeterType)cmbMeterType.SelectedItem;
                int meterId;

                if (!int.TryParse(txtMeterId.Text, out meterId))
                {
                    throw new Exception("Invalid meter ID.");
                }
                if (!dteDate.SelectedDate.HasValue)
                {
                    throw new Exception("Select a valid date.");
                }

                List<int> value = new();
                ConsumptionType consumptionType = ConsumptionType.OFFPEAKLOAD;
                foreach (TextBox child in stpControls.Children.OfType<TextBox>())
                {
                    value.Add(int.Parse(child.Text)); 
                    if (string.IsNullOrWhiteSpace(child.Text))
                    {
                        throw new Exception($"Invalid input for {child.Name}.");
                    }
                }

                foreach (ComboBox comboBox in stpControls.Children.OfType<ComboBox>())
                {
                    consumptionType = (ConsumptionType)comboBox.SelectedItem;
                    if (comboBox.SelectedItem == null)
                    {
                        throw new Exception($"Please select a valid option for {comboBox.Name}.");
                    }
                }

                if (selectedReading != null)
                {
                    meterAdmin.RemoveMeterReading(selectedReading);
                    MessageBox.Show("You have succesfully changed the meterReading", "MeterReading changed", MessageBoxButton.OK);
                }


                if (meterAdmin.MeterReadings.Any(r => r.MeterId == meterId && r != selectedReading))
                {
                    throw new Exception("A meter reading with this ID already exists.");
                }

                /*
                foreach (MeterReading existingReading in meterAdmin.MeterReadings)
                {
                    if (existingReading.MeterId == meterId)
                    {
                        throw new Exception("A meter reading with this ID already exists.");
                    }
                }
                */
                
                if (cmbMeterType.SelectedItem == null)
                {
                    throw new Exception("Please select a meter type.");
                }
                MeterReading newReading;

                switch (meterType)
                {
                    case MeterType.Water:
                        newReading = new Water
                            (
                                meterId, meterType, dteDate.SelectedDate.Value, value[0]
                            );
                        break;
                    case MeterType.AirQuality:
                        newReading = new AirQuality
                            (
                                meterId, meterType, dteDate.SelectedDate.Value, value[0], value[1]
                            );
                        break;
                    case MeterType.Electricity:
                        newReading = new Electricity
                            (
                                meterId, meterType, dteDate.SelectedDate.Value, value[0], consumptionType
                            );
                        break;
                    case MeterType.SolarPanel:
                        newReading = new Solar
                            (
                                meterId, meterType, dteDate.SelectedDate.Value, value[0]
                            );
                        break;
                    default:
                        throw new Exception("error");
                        break;
                }
                meterAdmin.AddMeterReading(newReading);



                if (selectedReading == null)
                {
                    int count = meterAdmin.GetMetersCount();
                    if (count <= 5)
                    {
                        MessageBox.Show($"You have added a reading, you now have {count} readings", "Reading added", MessageBoxButton.OK);
                    }
                }


                RefreshMeterReadingsList();
                ClearInputFields();

                
            }
            catch (Exception ex)
            {
                if (selectedReading != null)
                {
                    meterAdmin.AddMeterReading(selectedReading);
                }

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearInputFields()
        {
            txtMeterId.Text = string.Empty;
            dteDate.SelectedDate = null;
            cmbMeterType.SelectedItem = null;
            stpControls.Children.Clear();
            lstMeterReadings.SelectedIndex = -1;
            selectedReading = null;
        }
        
        private void RefreshMeterReadingsList()
        {
            lstMeterReadings.Items.Clear();

            foreach (MeterReading reading in meterAdmin.MeterReadings)
            {
                string displayText = $"{reading.MeterType} Meter ID: {reading.MeterId}, Date: {reading.Date.ToShortDateString()} : ";

                switch (reading.MeterType)
                {
                    case MeterType.AirQuality:
                        AirQuality airQuality = (AirQuality)reading;
                        displayText += $"CO2: {airQuality.CO2} ppm, PM2.5: {airQuality.PM25} µg/m³";
                        break;
                    case MeterType.SolarPanel:
                        Solar solar = (Solar)reading;
                        displayText += $"Energy Production: {solar.Energy} kWh";
                        break;
                    case MeterType.Electricity:
                        Electricity electricity = (Electricity)reading;
                        displayText += $"{electricity.ConsumptionType} Usage: {electricity.Energy} kWh";
                        break;
                    case MeterType.Water:
                        Water water = (Water)reading;
                        displayText += $"Water Usage: {water.Liters} liters";
                        break;
                }
                lstMeterReadings.Items.Add(displayText);
            }
        }

        private void AddControlToUI(string labelText, string textBoxName)
        {
            TextBlock textBlock = new TextBlock { Text = labelText, Margin = new Thickness(0, 0, 10, 0) };
            TextBox textBox = new TextBox { Name = textBoxName };
            textBox.PreviewTextInput += NumericTextBox_PreviewTextInput;
            stpControls.Children.Add(textBlock);
            stpControls.Children.Add(textBox);
        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }
        private bool IsTextNumeric(string text)
        {
            return double.TryParse(text, out _);
        }
        private void AddComboBoxToUI(string labelText, string comboBoxName)
        {
            TextBlock textBlock = new TextBlock { Text = labelText, Margin = new Thickness(0, 0, 10, 0) };
            ComboBox comboBox = new ComboBox
            {
                Name = comboBoxName,
                ItemsSource = new[] { ConsumptionType.PEAKLOAD, ConsumptionType.OFFPEAKLOAD }
            };
            stpControls.Children.Add(textBlock);
            stpControls.Children.Add(comboBox);
        }

        private void LstMeterReadings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstMeterReadings.SelectedItem != null)
            {
                string selectedDisplay = lstMeterReadings.SelectedItem.ToString();
                selectedReading = meterAdmin.MeterReadings.FirstOrDefault(r => selectedDisplay.Contains($"Meter ID: {r.MeterId}"));

                if (selectedReading != null)
                {
                    txtMeterId.Text = selectedReading.MeterId.ToString();
                    dteDate.SelectedDate = selectedReading.Date;
                    cmbMeterType.SelectedItem = Enum.Parse(typeof(MeterType), selectedReading.MeterType.ToString());

                    CmbMeterType_SelectionChanged(null, null);

                    Water waterReading;
                    Electricity electricityReading;
                    Solar solarReading;
                    AirQuality airQualityReading;

                    switch (selectedReading.MeterType)
                    {
                        case MeterType.Water:
                            waterReading = (Water)selectedReading;
                            foreach (TextBox control in stpControls.Children.OfType<TextBox>())
                            {
                                control.Text = waterReading.Liters.ToString();
                            }
                            break;
                        case MeterType.Electricity:
                            electricityReading = (Electricity)selectedReading;
                            foreach (TextBox control in stpControls.Children.OfType<TextBox>())
                            {
                                control.Text = electricityReading.Energy.ToString();
                            }
                            foreach (ComboBox control in stpControls.Children.OfType<ComboBox>())
                            {
                                    control.SelectedItem = electricityReading.ConsumptionType;
                            }
                            break;
                        case MeterType.SolarPanel:
                            solarReading = (Solar)selectedReading;
                            foreach (TextBox control in stpControls.Children.OfType<TextBox>())
                            {
                                control.Text = solarReading.Energy.ToString();
                            }
                            break;
                        case MeterType.AirQuality:
                            airQualityReading = (AirQuality)selectedReading;
                            int index = 0;
                            foreach (TextBox control in stpControls.Children.OfType<TextBox>())
                            {
                                if (index == 0)
                                {
                                    control.Text = airQualityReading.CO2.ToString();
                                }
                                if (index == 1)
                                {
                                    control.Text = airQualityReading.PM25.ToString();
                                }
                                index++;
                            }
                            break;
                    }




                    
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            { 
                case Key.Escape:
                    lstMeterReadings.UnselectAll();
                    ClearInputFields();
                    stpControls.Children.Clear();
                    break; 

                case Key.Delete:

                    MessageBoxResult result = MessageBox.Show("are you sure you wish to delete this reading?", "deleting", MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        meterAdmin.RemoveMeterReading(selectedReading);
                        
                        int count = meterAdmin.GetMetersCount();
                        MessageBox.Show($"You have deleted 1 reading, you now have {count} readings", "Reading deleted", MessageBoxButton.OK);
                        RefreshMeterReadingsList();
                        
                        ClearInputFields();
                        
                    }
                    else if (result == MessageBoxResult.Cancel)
                    {
                        break;
                    }
                    break;
            }
        }
    }
}
