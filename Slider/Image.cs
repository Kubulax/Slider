using System;
using System.Collections.Generic;
using System.Text;

namespace Slider
{
    public class Image
    {
        public string Name {  get; set; }
        public string Source { get; set; }

        public Image(string name, string source)
        {
            this.Name = name;
            this.Source = source;
        }

        public Image() 
        {

        }
    }
}
