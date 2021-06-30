using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using UI.Extentions;
using UI.Services;
using UI.Views;

namespace UI.ViewModels
{
    public class MainViewModel
    {
        private readonly PlaneView plane = new PlaneView();
        public Grid TrailGrid { get; set; }
        public PlaneViewModel Plane { get; set; }
        public TextBlock Flight { get; set; }
        private readonly bool[,] Trail = new bool[,]
        {
            { false, false, false, false, false },
            { true, true, true, true, true },
            { false, true, false, true, false },
            { false, false, true, true, false },
            { false, false, false, false, false },
        };


        public MainViewModel(SignalRAirportService signalRAirportService)
        {
            Plane = new PlaneViewModel();

            Flight = new TextBlock();

            Layouts();

            signalRAirportService.FlightReceived += new Action<FlightOnRoute>(SignalRAirportService_FlightReceived);
        }

        private void SignalRAirportService_FlightReceived(FlightOnRoute obj)
        {
            Flight.Text = obj.ToString();
        }

        private void Layouts()
        {
            //Trail =  _logic.GetTrail();

            TrailGrid = new Grid();

            for (int row = 0; row < Trail.GetLength(0); row++)
            {
                TrailGrid.RowDefinitions.Add(new RowDefinition());
                for (int col = 0; col < Trail.GetLength(1); col++)
                {
                    if (row == 0)
                        TrailGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    AddCanvas(row, col);
                }
            }

            AddToGrid(plane, 1, 4);
        }

        private void AddCanvas(int row, int col)
        {
            Canvas canvas = new Canvas { Style = Trail[row, col] ? StyleEx.Path : StyleEx.Wall };

            AddToGrid(canvas, row, col);
        }

        private void AddToGrid(UIElement element, int row, int col)
        {
            Grid.SetRow(element, row);
            Grid.SetColumn(element, col);
            TrailGrid.Children.Add(element);
        }
    }
}
