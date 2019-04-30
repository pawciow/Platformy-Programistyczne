using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Lab01
{
    public partial class ChartWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public ColumnSeries ColumnSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        Database1Entities context = new Database1Entities();

        public ChartWindow(List<float> rates)
        {
            InitializeComponent();
            ChartInitialize(rates);
            DataContext = this;
        }

        public void ChartInitialize(List<float> rates)
        {
            var LabelsList = new List<string>();

            SeriesCollection = new SeriesCollection();

            ColumnSeries = new ColumnSeries
            {
                Title = "Rating",
                Values = new ChartValues<float>()
            };

            Formatter = value => value.ToString("N");

            foreach (var x in rates)
            {
                ColumnSeries.Values.Add(x);
                LabelsList.Add(x.ToString());
            }

            SeriesCollection.Add(ColumnSeries);
            Labels = LabelsList.ToArray();
        }

    }
}