using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace UI.Extentions
{
    public static class StyleEx
    {
        public static Style Plane = (Style)App.Current.Resources["plane"];
        public static Style Wall = (Style)App.Current.Resources["wall"];
        public static Style Path = (Style)App.Current.Resources["path"];
    }
}
