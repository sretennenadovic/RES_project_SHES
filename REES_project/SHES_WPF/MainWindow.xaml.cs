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
        //public double[] batteryDbValues = null;
        static List<double> batteryDbValues = new List<double>();
        static List<double> panelDbValues = new List<double>();
        static List<double> consumersDbValues = new List<double>();
        static List<double> utilityDbValues = new List<double>();
        static DateTime Start ;

        List<KeyValuePair<DateTime, double>>  listBattery = new List<KeyValuePair<DateTime, double>>();
        List<KeyValuePair<DateTime, double>> listPanel = new List<KeyValuePair<DateTime, double>>();
        List<KeyValuePair<DateTime, double>> listConsumers = new List<KeyValuePair<DateTime, double>>();
        List<KeyValuePair<DateTime, double>> listUtility = new List<KeyValuePair<DateTime, double>>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Load()
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Process shesConAppProces = new Process();
            shesConAppProces.StartInfo.FileName = @"..\..\..\SHES_ConsolApp\bin\Debug\SHES_ConsolApp.exe";
            shesConAppProces.Start();
        }



        private void LoadBarChartData(string formatted)
        {
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
            }

            //da bi bilo optimizovanije saljem true ili false jer se poslednja malo razlikuje,
            //ali u sustini koristi isti kod pa da ne bih kucao 2 identicne metode
            GetKeyValuePairs(batteryDbValues, listBattery,false);
            GetKeyValuePairs(panelDbValues, listPanel, false);
            GetKeyValuePairs(consumersDbValues,listConsumers, false);
            GetKeyValuePairs(utilityDbValues,listUtility, true);

            ((LineSeries)sr.Series[0]).ItemsSource = listPanel.ToArray();
            ((LineSeries)sr.Series[1]).ItemsSource = listBattery.ToArray();
            ((LineSeries)sr.Series[2]).ItemsSource = listUtility.ToArray();
            ((LineSeries)sr.Series[3]).ItemsSource = listConsumers.ToArray();


            //GetKeyValuePairs()

            /* DateTime from = Convert.ToDateTime(formatted + " 00:00");
             DateTime to = Convert.ToDateTime(formatted + " 23:59");

             var con = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=InfosDb; Integrated Security = true";


             using (SqlConnection myConnection = new SqlConnection(con))
             {
                 string batteryString = $"Select BatteryPower from Infos where Time>{from}&&Time<{to}";
                 string panelString = $"Select PanelPower from Infos where Time>{from}&&Time<{to}";
                 string consumersString = $"Select ConsumersPower from Infos where Time>{from}&&Time<{to}";
                 string utilityString = $"Select UtilityMoney from Infos where Time>{from}&&Time<{to}";

                 SqlCommand oCmd = new SqlCommand(batteryString, myConnection);
                 SqlCommand oCmd = new SqlCommand(batteryString, myConnection);
                 SqlCommand oCmd = new SqlCommand(batteryString, myConnection);
                 SqlCommand oCmd = new SqlCommand(batteryString, myConnection);
                 oCmd.Parameters.AddWithValue("@Fname", fName);
                 myConnection.Open();
                 using (SqlDataReader oReader = oCmd.ExecuteReader())
                 {
                     while (oReader.Read())
                     {
                         matchingPerson.firstName = oReader["FirstName"].ToString();
                         matchingPerson.lastName = oReader["LastName"].ToString();
                     }

                     myConnection.Close();
                 }
             }*/


            //  ((LineSeries)sr.Series[0]).ItemsSource = lista;

            // }

            /*      ((LineSeries)sr.Series[1]).ItemsSource =
                  new KeyValuePair<DateTime, int>[]{
              new KeyValuePair<DateTime,int>(DateTime.Now, 200),
                      new KeyValuePair<DateTime, int>(DateTime.Now.AddHours(1),200),
                      new KeyValuePair<DateTime, int>(DateTime.Now.AddHours(2),500),
                      new KeyValuePair<DateTime, int>(DateTime.Now.AddHours(3),0)};

                  ((LineSeries)sr.Series[2]).ItemsSource =
                     new KeyValuePair<DateTime, int>[]{
              new KeyValuePair<DateTime,int>(DateTime.Now, 0),
                      new KeyValuePair<DateTime, int>(DateTime.Now.AddHours(1),100),
                      new KeyValuePair<DateTime, int>(DateTime.Now.AddHours(2),200),
                      new KeyValuePair<DateTime, int>(DateTime.Now.AddHours(3),300)};

                  ((LineSeries)sr.Series[3]).ItemsSource =
                          new KeyValuePair<DateTime, int>[]{
              new KeyValuePair<DateTime,int>(DateTime.Now, 100),
                      new KeyValuePair<DateTime, int>(DateTime.Now.AddHours(1),-100),
                      new KeyValuePair<DateTime, int>(DateTime.Now.AddHours(2),100),
                      new KeyValuePair<DateTime, int>(DateTime.Now.AddHours(3),-100)};*/
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
   

          /*  double rez = 0;
            int counter = 0;

            for (int i = 1; i <= array.Count(); i++)
            {
                rez += array[i-1];
                if (i % 8 == 0 && i!=0)
                {
                    counter++;
                    rez = rez / 8;
                    list.Add(new KeyValuePair<DateTime, double>(DateTime.Now.AddHours(counter), rez));
                    rez = 0;
                }
                if ((array.Count() - (counter * 8)) < 8)
                {
                    for (int j = (counter * 8)+1; j <= array.Count(); j++)
                    {
                        rez += array[j-1];
                        if (j == array.Count())
                        {
                            //trazimo prosek za ta merenja koja nisu do punog sata (ovde uzeto 8 merenja da je sat)
                            rez = rez / (array.Count() - (counter * 8));
                            //posto name je to prosek za sat moramo pomnoziti sa brojem merenja (koja su manje od 1 sata)
                            //i podeliti sa brojem merenja u celom satu, da bi dobili koliko je zaista proizvedeno u
                            //periodu manjem od sata
                            rez = rez * (array.Count() - (counter * 8)) / 8;
                            list.Add(new KeyValuePair<DateTime, double>(DateTime.Now.AddHours(counter), rez));
                        }
                    }
                    break;
                }
            }*/
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
        }
    }
}
