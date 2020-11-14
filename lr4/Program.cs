using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace lr4
{
    
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var sll = new SLL<int> { 1, 2, 4, 5 };


                var sdd = new SLL<Person> { new Person("Егор"), new Person("Евгений") };


                string s = sdd.ToJson();

                Console.WriteLine(s);
                Console.WriteLine(SLL<Person>.FromJson(s));
  
               

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Прощай мир");
            }
        

         
 

        }
    }
}
