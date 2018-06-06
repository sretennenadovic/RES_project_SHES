using DBProject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SHES_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Drawing

        static List<double> batteryDbValues = new List<double>();
        static List<double> panelDbValues = new List<double>();
        static List<double> consumersDbValues = new List<double>();
        static List<double> utilityDbValues = new List<double>();
        static List<double> tempUtilityMoney = new List<double>();
        static DateTime Start;


        List<KeyValuePair<DateTime, double>> listBattery = new List<KeyValuePair<DateTime, double>>();
        List<KeyValuePair<DateTime, double>> listPanel = new List<KeyValuePair<DateTime, double>>();
        List<KeyValuePair<DateTime, double>> listConsumers = new List<KeyValuePair<DateTime, double>>();
        List<KeyValuePair<DateTime, double>> listUtility = new List<KeyValuePair<DateTime, double>>();
        List<KeyValuePair<DateTime, double>> listUtilityMoney = new List<KeyValuePair<DateTime, double>>();
        #endregion

        #region Process
        Process shesConAppProces;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //startujemo proces consol app shes
            shesConAppProces = new Process();
            shesConAppProces.StartInfo.FileName = @"..\..\..\SHES_ConsolApp\bin\Debug\SHES_ConsolApp.exe";
            shesConAppProces.Start();
        }



        private void LoadBarChartData(string formatted)
        {
            //moramo prvo formatirati datum zbog baze
            DateTime from = Convert.ToDateTime(formatted + " 00:00");
            DateTime to = Convert.ToDateTime(formatted + " 23:59");

            using (var context = new InfosDbContext())
            {
                var query = context.Infos.Where(s => s.Time >= from && s.Time <= to );
                Start = query.Select(x => x.Time).Min();

                batteryDbValues = (query.Select(x => x.BatteryPower)).ToList();
                panelDbValues = (query.Select(x => x.PanelPower)).ToList();
                consumersDbValues = (query.Select(x => x.ConsumersPower)).ToList();
                utilityDbValues = (query.Select(x => x.UtilityMoney)).ToList();
                tempUtilityMoney = (query.Select(x => x.UtilityMoney)).ToList();
            }

            //da bi bilo optimizovanije saljem true ili false jer se poslednja malo razlikuje,
            //ali u sustini koristi isti kod pa da ne bih kucao 2 identicne metode
            GetKeyValuePairs(batteryDbValues, listBattery,false);
            GetKeyValuePairs(panelDbValues, listPanel, false);
            GetKeyValuePairs(consumersDbValues,listConsumers, false);
            GetKeyValuePairs(utilityDbValues,listUtility, true);
            GetKeyValuePairs(tempUtilityMoney,listUtilityMoney, false);

            ((LineSeries)sr.Series[0]).ItemsSource = listPanel.ToArray();
            ((LineSeries)sr.Series[1]).ItemsSource = listBattery.ToArray();
            ((LineSeries)sr.Series[2]).ItemsSource = listUtility.ToArray();
            ((LineSeries)sr.Series[3]).ItemsSource = listConsumers.ToArray();
        }


        private void GetKeyValuePairs(List<double> array, List<KeyValuePair<DateTime, double>> list,bool bol)
        {
            int cnt = 0;
            for (int i = 0; i < array.Count; i++)
            {
                if(i%8==0 && i!=0)
                {
                    double value;
                    if (bol)
                    {
                        value = array.Take(8).Average()/10;//delimo sa cenom
                                                           //da bi dobili kwh (jer utility vraca cenu a na grafiku
                                                     //cemo prikazati potraznju za kwh)
                    }
                    else
                    {
                        value = array.Take(8).Average();
                    }
                    list.Add(new KeyValuePair<DateTime, double>(Start.AddHours(++cnt), value));
                    array.RemoveRange(0, 8);
                    i = 0;
                }else if (i  == (array.Count - 1) )
                {
                    double value;
                    if (bol)
                    {
                        value = (array.Average()) * ((double)i / 8) / 10;
                    }
                    else
                    {
                        value = (array.Average()) * ((double)i / 8);
                    }
                    list.Add(new KeyValuePair<DateTime, double>((Start.AddHours(cnt + (double)i / 8)), value));
                    array.Clear();
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string formatted = null;
            DateTime? selectedDate = Date.SelectedDate;
            if (selectedDate.HasValue)
            {
                formatted = selectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            LoadBarChartData(formatted);

            double price = 0;

            foreach (KeyValuePair<DateTime,double> item in listUtilityMoney)
            {
                price += item.Value;
            }

            price = price / listUtilityMoney.Count;
            label1.Content = "Money which you earned/spent ";
            label3.Content = $"on energy on date {Start.Date.Day}.{Start.Date.Month}.{Start.Date.Year}:";
            label2.Content = Convert.ToString(price+" $");

            listUtilityMoney.Clear();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process process in runningProcesses)
            {
                if (process.ProcessName.Equals("Battery") || process.ProcessName.Equals("Utility") || process.ProcessName.Equals("SolarPanel") || process.ProcessName.Equals("Consumer"))
                    process.Kill();
            }
            shesConAppProces.Kill();
        }
    }
}
