using Keil.DynamicProperties;
using Keil.DynamicProperties.Serialization;
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


            var obj2 = new object();
            //Add prop to instance
            int propVal2 = 1;
            var propManager2 = new DynamicPropertyManager<object>(obj2);
            propManager2.Properties.Add(
                DynamicPropertyManager<object>.CreateProperty<object, int>(
                "Value2", t => propVal2, (t, y) => propVal2 = y, null));

            propVal2 = 7;

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new TypeDescriptorContractResolver(),
            };

            //Serialize object here
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);

            Console.WriteLine(json);
            
            var json2 = JsonConvert.SerializeObject(obj2, Formatting.Indented, settings);
           

            Console.WriteLine(json2);

            Console.ReadKey();

           
        }
    }
}
