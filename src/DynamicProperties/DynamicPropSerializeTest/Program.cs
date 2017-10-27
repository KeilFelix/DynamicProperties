using Keil.DynamicProperties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace DynamicPropSerializeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //For Question
            //https://stackoverflow.com/questions/46888732/how-to-serialize-runtime-added-properties-to-json

            var obj = new object();

            //Add prop to instance
            int propVal = 0; 
            var propManager = new DynamicPropertyManager<object>(obj);
            propManager.Properties.Add(
                DynamicPropertyManager<object>.CreateProperty<object, int>(
                "Value", t => propVal, (t, y) => propVal = y, null));

            propVal = 3;

            //Serialize object here
            var json = JsonConvert.SerializeObject(obj);
            //Deserialize

            Console.WriteLine(json);
            Console.ReadKey();

           
        }
    }
}
