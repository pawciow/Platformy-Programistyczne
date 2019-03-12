using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
using System.IO;

namespace Lab01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>d
    public partial class MainWindow : Window
    {
        BitmapImage _Image = new BitmapImage { };
        ObservableCollection<Person> people = new ObservableCollection<Person>
        {
            new Person { Name = "P1", Age = 1, Picture = new BitmapImage(new Uri("C:\\Users\\Pawel\\Desktop\\dotNet\\Lab01\\Properties\\lena.bmp"))}, // Zmien sobie na jakis swoj obraz u siebie:P
            new Person { Name = "P2", Age = 2, Picture =  new BitmapImage(new Uri("C:\\Users\\Pawel\\Desktop\\dotNet\\Lab01\\Properties\\lena.bmp"))}
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
    }
}