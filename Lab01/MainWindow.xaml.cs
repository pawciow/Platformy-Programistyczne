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
using System.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core;
namespace Lab01
{
 

    public partial class MainWindow : Window
    {
        CollectionViewSource tableViewSource;
        public MainWindow()
        {

            InitializeComponent();
            tableViewSource = ((CollectionViewSource)(FindResource("tableViewSource")));
            DataContext = this;
        }


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource tableViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tableViewSource")));
            // Załaduj dane poprzez ustawienie właściwości CollectionViewSource.Source:
            // tableViewSource.Źródło = [ogólne źródło danych]
            //context.Table.Load();
            tableViewSource.Source = context.Table.ToList();
        }

        private void AddFilmButton_Click(object sender, RoutedEventArgs e)
        {
            /*ADD_STH*/
        }

        private void TableListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        Database1Entities context = new Database1Entities();
        async private void AddRandomMovie_Click(object sender, RoutedEventArgs e)
        {
            GetRandomMovie random = await MovieConentGetter.GetApiAsync();
            string toShow = "Title : " + random.original_title + "\n" + "Ratings: " + random.vote_average + " \n Overview:" + random.overview;
            MessageBox.Show(toShow, "Random movie selected");

            /*Dodawanko*/
            Movie movie = new Movie { Name = random.original_title, Rating = random.vote_average };

            await AddMovieToDatabase(movie);
            PrintAllContentToConsole();

            DeleteMovie(movie);
            PrintAllContentToConsole();
        }

        async private void DeleteMovie(Movie movie)
        {
            var query = (from tmp in context.Table
                         where tmp.Title == movie.Name
                         select tmp).Single();
            Console.WriteLine("ID: {0}, Title = {1}", query.Id, query.Title);
            context.Table.Remove(query);
            await context.SaveChangesAsync();
        }

        private async Task AddMovieToDatabase(Movie movie)
        {
            await Task.Delay(2000);
            var toAdd = new Lab01.Table { Id = 2, Title = movie.Name, Date_of_production = movie.Date_of_production };
            context.Table.Add(toAdd);
            await context.SaveChangesAsync();
        }

        async private void PrintAllContentToConsole()
        {
            /* Wyświetlanie wszystkiego */
            await Task.Delay(2000);

            var query = from tmp in context.Table select tmp;
            foreach (var a in query)
            {
                Console.WriteLine("ID: {0}, Title = {1}", a.Id, a.Title);
            }
            Console.WriteLine(query);
        }
    }

 

}