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
using System.Data.Entity;

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
            RootObject random = await MovieConentGetter.GetApiAsync();
            foreach(Result result in random.results)
            {
                //string toShow = "Title : " + result.original_title + "\n" + "Ratings: " + result.vote_average + " \n Overview:" + result.overview;
                //MessageBox.Show(toShow, "Random movie selected");
                Movie movie = new Movie { Name = result.original_title, Rating = result.vote_average };
                await AddMovieToDatabase(movie);

            }

            PrintAllContentToConsole();
        }

        async private void DeleteMovie(Movie movie)
        {
            var query = (from tmp in context.Table
                         where tmp.Title == movie.Name
                         select tmp).Single();
            Console.WriteLine("ID: {0}, Title = {1}, Rating = {2}", query.Id, query.Title, query.Rating);
            context.Table.Remove(query);
            await context.SaveChangesAsync();
        }

        private static int ID = 1;
        private async Task AddMovieToDatabase(Movie movie)
        {
            await Task.Delay(2000);
            var toAdd = new Lab01.Table { Id = ID, Title = movie.Name, Date_of_production = movie.Date_of_production, Rating = movie.Rating };
            context.Table.Add(toAdd);
            await context.SaveChangesAsync();
            ID++;
            tableViewSource.Source = context.Table.ToList();
        }

        async private void PrintAllContentToConsole()
        {
            /* Wyświetlanie wszystkiego */
            await Task.Delay(2000);

            var query = from tmp in context.Table select tmp;
            foreach (var a in query)
            {
                Console.WriteLine("ID: {0}, Title = {1}, Rating = {2}", a.Id, a.Title, a.Rating);
            }
            Console.WriteLine(query);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            context.SaveChanges();
        }

        async private void AddMovieToDatabase(object sender, RoutedEventArgs e)
        {
            var newEntry = new Movie
            {
                Name = titleTextBox2.Text,
                Rating = float.Parse(ratingTextBox2.Text),
            };

            await AddMovieToDatabase(newEntry);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong...", ex.Message);
            }
        }
    }

 

}