using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace lr4
{

    public class Person 
    {

        public Person() => Name = string.Empty;
        public Person(string name) => Name = name;
        public Person(string name, DateTime birthday) : this(name) => Birhday = birthday;
        public string Name { get; set; }

        public DateTime Birhday { get; set; } = RandomDay.Get(new DateTime(2000, 10, 11), DateTime.Today);

        [JsonIgnore]
        public string Phrase { get; set; } = "Здрасьте";
        public void Say() => Console.WriteLine(Phrase);

      

        public override string ToString() => $"Человек по имени {Name} с днем рождения {Birhday.ToString("d")}";

 


        //$"{{Имя: {p.Name}, Дата рождения: {p.Birhday.ToString("d")}}}",
        public static Person Parse(string s)
        {
            var tokens = s.Split(", ", StringSplitOptions.RemoveEmptyEntries);

            

            return new Person(tokens[0].Split(": ", StringSplitOptions.RemoveEmptyEntries)[1]
                , DateTime.Parse(tokens[1].Split(": ", StringSplitOptions.RemoveEmptyEntries)[1]));

        }
    }

}
