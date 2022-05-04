using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Weather
{
    class Translate
    {
        string word;
        public Translate(string word)
        {
            this.word = word;
        }

        public string GetWord()
        {
            switch (word)
            {
                case "Clouds":
                    word = "Облачно";
                    break;
                case "Clear":
                    word = "Безоблачно";
                    break;
                case "Rain":
                    word = "Дождь";
                    break;
                case "Snow":
                    word = "Снег";
                    break;
                case "Sunny":
                    word = "Солнечно";
                    break;
            }
          
            return word;
        }
    }
}
