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
        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource tableViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tableViewSource")));
            // Załaduj dane poprzez ustawienie właściwości CollectionViewSource.Source:
            // tableViewSource.Źródło = [ogólne źródło danych]
        }

        private void AddFilmButton_Click(object sender, RoutedEventArgs e)
        {
            /*ADD_STH*/
        }

        private void TableListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        async private void AddRandomMovie_Click(object sender, RoutedEventArgs e)
        {
            GetRandomMovie random = await MovieConentGetter.GetApiAsync();
            string toShow = "Title : " + random.original_title + "\n" + "Ratings: " + random.vote_average + " \n Overview:" + random.overview;
            MessageBox.Show(toShow, "Random movie selected");
            Movie movie = new Movie { Name = random.original_title, Rating = random.vote_average };
            var toAdd = new Lab01.Table { Id = 1, Title = movie.Name, Date_of_production = movie.Date_of_production};
            using (var context = new Database1Entities())
            {
                context.Table.Add(toAdd);
                context.SaveChanges();
            }
        }
    }

 

}