using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using UI.Extentions;

namespace UI.ViewModels
{
    public class PlaneViewModel
    {
        readonly PlaneUIGenerator planeUIGenerator = new PlaneUIGenerator();
        public Grid PlaneGrid { get; set; }

        public PlaneViewModel()
        {
            PlaneGrid = new Grid();
            LayoutDesign();
            AddRectangles();
        }

        private void AddRectangles()
        {
            foreach (var location in planeUIGenerator.GetLocations())
            {
                Button part = new Button { Background = new SolidColorBrush(Colors.Black) };
                Grid.SetRow(part, location.Row);
                Grid.SetColumn(part, location.Column);
                PlaneGrid.Children.Add(part);
            }

        }

        private void LayoutDesign()
        {
            for (int i = 0; i < 3; i++)
            {
                PlaneGrid.ColumnDefinitions.Add(new ColumnDefinition());
                PlaneGrid.RowDefinitions.Add(new RowDefinition());
            }
        }
    }
}
