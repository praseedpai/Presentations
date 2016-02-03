using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ElaborateSQL
{
    class  Program
    {

        public static void SQLString()
        {
            TSQLAccess ts = new TSQLAccess(GetConStr());

            string query = @"select ar.A_CODE,br.S_CODE,br.S_DESC from FaGroup ar,FaSubgroup br " +
                @"where ar.A_CODE = substring(S_CODE,1,2) ;";
            ts.Open();
            DataSet ds = ts.Execute(query);
            DataTable dt = ds.Tables[0];


            int rs = dt.Rows.Count;

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr[0].ToString() + " " + dr[1].ToString() + " " + dr[2].ToString());

            }
            ts.Close();

        }

        public static void SubQuery2()
        {

            FA2 fe = new FA2(GetConStr());
            IEnumerable<FASubGroup> c = from n in fe.FASubGroup
                    select n;

            foreach (FASubGroup r in c)
            {
                Console.WriteLine(r.S_CODE + "  " + r.S_DESC);

            }

        }
        

        public static void SubQuery()
        {
            FA2 fe = new FA2(GetConStr());


            var c = from n in fe.FAGroup
                    select
                    new
                    {
                        n.A_CODE,
                        Desc = from d in fe.FASubGroup
                        where d.S_CODE.Substring(0, 2) == n.A_CODE
                        select new { d.S_CODE, d.S_DESC }
                    };


            foreach (var t in c)
            {
                foreach (var r in t.Desc)
                    Console.WriteLine(t.A_CODE +" "+r.S_CODE +" "+ r.S_DESC);

               
            }
        }

        public static void TestQuery()
        {

            FA2 fe = new FA2(GetConStr());

           // IEnumerable<int> next_j = from n in fe.JournalMaster
             //            orderby n.J_ID
                //         select Int32.Parse(n.J_ID);
           // int  rs = next_j.ElementAt(0);

            var test = from n in fe.FAGroup
                       select n;
            foreach (var c in test)
            {
                Console.WriteLine(c.A_CODE + " " + c.A_DESC);

            }



           // Console.WriteLine(rs);
                         
        }

        public static string GetConStr()
        {

            return @"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=FA;Integrated Security=True";
        }

        public static void SimpleQuery()
        {
            FA2 fe = new FA2(GetConStr());


            var c = from n in fe.FAGroup
                    select
                    new
                    {
                        n.A_CODE,
                        n.A_DESC
                    };



            foreach (var t in c)
            {
                
                    Console.WriteLine(t.A_CODE +" " + t.A_DESC);

              
            }
        }


        static void Main(string[] args)
        {
           //SubQuery();
          // Console.WriteLine("-------");
           // SimpleQuery();
          //  SQLString();

            TestQuery();

            Console.Read();

        }
    }
}
