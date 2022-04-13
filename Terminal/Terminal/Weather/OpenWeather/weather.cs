using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Terminal.OpenWeather
{
    class weather
    {
        public int id;

        public string main;

        public string description;

        public string icon;

        public BitmapImage Icon
        {
            get
            {
                return new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/icons/{icon}.png"));
            }
        }

        public Bitmap Background
        {
            get
            {
                return new Bitmap(Image.FromFile($"Background/{icon}.jpg"));
            }
        }
    }
}
