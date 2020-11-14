using Newtonsoft.Json;
using System.IO;


namespace lr4
{
    public static class Extension
    {
        public static string ToJson<T>(this ICollection<T> collection) 
            => JsonConvert.SerializeObject(collection);


        public static void ToJsonFile<T>(this ICollection<T> collection, string fileName)
            => File.WriteAllText(fileName, ToJson(collection));


    }
}
