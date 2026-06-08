using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace RenegadeCharacterBuilder.GlobalMethods
{
    public class GlobalCall
    {
        public T? LoadJson<T>(string jsonLocation, string gamejsonfolder)
        {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jsoncollection", gamejsonfolder, jsonLocation);
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
