using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab01
{
 

    public partial class MainWindow : Window
    {
        BitmapImage _Image = new BitmapImage { };
       
        public const string imageName = "potato.jpg";
        public const string url = "https://uinames.com/api/?ext";


        ObservableCollection<Person> people = new ObservableCollection<Person>
        {
         //  new Person { Name = "P1", Age = 1, Picture = new BitmapImage(new Uri("C:\\Users\\Pawel\\Desktop\\dotNet\\Lab01\\Properties\\lena.bmp"))}, // Zmien sobie na jakis swoj obraz u siebie:P
          //  new Person { Name = "P2", Age = 2, Picture =  new BitmapImage(new Uri("C:\\Users\\Pawel\\Desktop\\dotNet\\Lab01\\Properties\\lena.bmp"))}

           new Person { Name = "P1", Age = 1, Picture = new BitmapImage(new Uri("C:\\Users\\kamil\\source\\repos\\Platformy-Programistyczne\\potato.jpg"))},
           new Person { Name = "P2", Age = 2, Picture =  new BitmapImage(new Uri("C:\\Users\\kamil\\source\\repos\\Platformy-Programistyczne\\potato.jpg"))}
        };

        public ObservableCollection<Person> Items
        {
            get => people;
        }

        public object ProgresChanged { get; private set; }


        public MainWindow()
        {

            InitializeComponent();

            DataContext = this;
        }

        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                people.Add(new Person { Age = int.Parse(ageTextBox.Text), Name = nameTextBox.Text, Picture = _Image });

               
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Nieprawidłowy format w tabelce wiek");
            }

        }




        void OnImageButtonClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".png";
            fileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            if ((bool)fileDialog.ShowDialog())
            {
                string fileName = fileDialog.FileName;

                PictureBox.Source = new BitmapImage(new Uri(fileName));
                _Image = new BitmapImage(new Uri(fileName));
            }

        }

        private void ReportProgress(object sender, int e)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    progressBar.Value = e;
                });
            }
            catch { }
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        private async void AddTextButton_Click(object sender, RoutedEventArgs e)
        {
            Progress<int> progress = new Progress<int>();
            progress.ProgressChanged += ReportProgress;
            GetRandomPerson random = await GetApiAsync("https://uinames.com/api/?ext", progress);
        }

        private async Task<GetRandomPerson> GetApiAsync(string path, IProgress<int> progress)
        {
             int report = new int();
            GetRandomPerson random = null;
            int levelmax = 5;
            int presentLevel = 0;
            while(presentLevel<=5)
            
            while (true)
            {
                presentLevel++;
                report = (presentLevel * 100) / levelmax;
                progress.Report(report);
                using (HttpClient client = new HttpClient())
                {

                    using (HttpResponseMessage response = await client.GetAsync(path))
                    {
                        using (HttpContent content = response.Content)
                        {
                            var stringContent = await content.ReadAsStringAsync();
                            random = JsonConvert.DeserializeObject<GetRandomPerson>(stringContent);


                            try
                            {
                                people.Add(new Person { Age = random.age, Name = random.name, Picture = new BitmapImage(new Uri(random.photo)) });
                            }
                            catch (System.FormatException)
                            {
                                Console.WriteLine("Nieprawidłowe dane");
                            }

                        }

                    }

                }
                Thread.Sleep(1000);


            }

            return random;


        }


    }

 

}