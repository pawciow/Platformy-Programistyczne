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

namespace Lab01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>d
    /// 

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

                // ageTextBox.Text = string.Empty;
                //  nameTextBox.Text = string.Empty;
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

        private async void AddTextButton_Click(object sender, RoutedEventArgs e)
        {
            GetRandomPerson random = await GetApiAsync("https://uinames.com/api/?ext");
        }

        async Task<GetRandomPerson> GetApiAsync(string path)
        {
            GetRandomPerson random;
            while (true)
            {
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

          


        }

    }

}