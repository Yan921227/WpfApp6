using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string defaultURL = "https://data.moenv.gov.tw/api/v2/aqx_p_432?api_key=e8dd42e6-9b8b-43f8-991e-b3dee723a52d&limit=1000&sort=ImportDate desc&format=JSON ";
        AQIdata aqidata =new AQIdata();
        List<Field> fields = new List<Field>();
        List<Record> records = new List<Record>();
        public MainWindow()
        {
            InitializeComponent();
            UrlTexBox.Text = defaultURL;
        }

        private async void FetchDataButton_Click(object sender, RoutedEventArgs e)
        {
            ContentTextBox.Text = "抓取網路資料中....";

            string jsonData = await FetchContentAsyns(defaultURL);
            ContentTextBox.Text = jsonData;
            aqidata = JsonSerializer.Deserialize<AQIdata>(jsonData);
            fields = aqidata.fields.ToList(); 
            records= aqidata.records.ToList();
            StatusTextBlock.Text=$"共有{records.Count}筆資料";
            DisplayAQIdata();
        }

        private void DisplayAQIdata()
        {
            RecordDataGrid.ItemsSource = records;

            Record record = records [0];
            DataWrapPanel.Children.Clear();

            foreach (Field field in fields)
            { 
             
            }
        }

        private async Task<string> FetchContentAsyns(string url) //回傳字串
        {
            try 
            {
                using (HttpClient client = new HttpClient())
                {
                    return await client.GetStringAsync(url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                return $"{ex.Message}";
            }
        }
    }
}
