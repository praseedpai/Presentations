using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace Cricket
{
    public class Cricketer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int[] Scores { get; set; }
    }



    class Program
    {
        static void Main(string[] args)
        {
            ArrayList arrList = new ArrayList();
            arrList.Add(
                new Cricketer
                {
                    FirstName = "Sachin",
                    LastName = "Tendulkar",
                    Scores = new int[] { 98, 92, 0, 14 }
                });
            arrList.Add(
                new Cricketer
                {
                    FirstName = "Saurav",
                    LastName = "Ganguly",
                    Scores = new int[] { 75, 84, 91, 39 }
                });
            arrList.Add(
                new Cricketer
                {
                    FirstName = "Rahul",
                    LastName = "Dravid",
                    Scores = new int[] { 8, 19, 65, 39 }
                });
            arrList.Add(
                new Cricketer
                {
                    FirstName = "VVS",
                    LastName = "Lakshman",
                    Scores = new int[] { 47, 89, 32, 27 }
                });

            var query = from Cricketer player in arrList
                        where player.Scores.Max()  > 50
                        select player;

            foreach (Cricketer s in query)
                Console.WriteLine(s.LastName + ": " + s.Scores.Max());

            Console.Read();


        }
    }
}
