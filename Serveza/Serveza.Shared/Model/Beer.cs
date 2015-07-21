using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Model
{
    public class Beer
    {
        public string name { get; private set; }
        public double degre { get; private set; }
        public string product { get; private set; }
        public Beer(string name, double degre, string product)
        {
            this.name = name;
            this.degre = degre;
            this.product = product;
        }
    }
}
