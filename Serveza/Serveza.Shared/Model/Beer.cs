using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Serveza.Model
{
    public class Beer
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public double degre { get; private set; }
        public string product { get; private set; }
        public double price { get; private set; }
        public Beer(string name, double degre, string product)
        {
            this.name = name;
            this.degre = degre;
            this.product = product;
        }

        public Beer() { }
        public void load(JObject obj)
        {
            try
            {
                Debug.WriteLine("load");
                name = obj["name"].ToObject<string>();
                string[] posStringSplit = obj["price"].ToObject<string>().Split(' ');
                price = Convert.ToDouble(posStringSplit[1]);
                id = obj["beer_id"].ToObject<int>();

                Debug.WriteLine(name);
                Debug.WriteLine(price);
                Debug.WriteLine(id);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("beer" + ex.ToString());
            }
        }

        public void GetInfo()
        {
            
        }
    }
}
