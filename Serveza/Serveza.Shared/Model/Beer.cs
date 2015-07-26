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
        public string image { get; private set; }
        public string desc { get; private set; }
        public double degre { get; private set; }
        public string product { get; private set; }
        public double price { get; private set; }
        public Beer(string name, double degre, string product)
        {
            this.name = name;
            this.degre = degre;
            this.product = product;
        }

        public Beer() { price = 0; }
        public void load(JObject obj)
        {
            try
            {
                Debug.WriteLine("load Beer");
                name = obj["name"].ToObject<string>();
                try
                {
                    string[] posStringSplit = obj["price"].ToObject<string>().Split(' ');
                    price = Convert.ToDouble(posStringSplit[1]);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                id = obj["id"].ToObject<int>();

                Debug.WriteLine("name : " + name);
                Debug.WriteLine(price);
                Debug.WriteLine("id : " + id.ToString());

            }
            catch (Exception ex)
            {
                Debug.WriteLine("beer" + ex.ToString());
            }
        }

        public void GetInfo(JObject obj)
        {
            try
            {
                degre = obj["degree"].ToObject<double>();
                desc = obj["description"].ToObject<string>();
                image = obj["image"].ToObject<string>();
                product = obj["brewery"].ToObject<string>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public bool isFav { get; set; }

        public override string ToString()
        {
            return "";
        }
    }
}
