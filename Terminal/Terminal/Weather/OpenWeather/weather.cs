using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Terminal.OpenWeather
{
    class weather
    {
        public int id;

        public string main;

        public string description;

        public string icon;

        public Bitmap Icon
        {
            get
            {
                return new Bitmap(Image.FromFile($"icons/{icon}.png"));
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
