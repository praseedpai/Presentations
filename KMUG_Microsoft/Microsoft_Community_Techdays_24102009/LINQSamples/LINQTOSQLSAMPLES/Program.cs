using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
 

namespace LINQTOSQLSAMPLES
{
    class Program
    {
        public static void PersistXML()
        {
            XDocument customer =
            new XDocument(
            new XDeclaration("1.0", "UTF-16", "yes"),
            new XElement("customer",
            new XAttribute("id", "C01"),
            new XElement("firstName", "Paolo"),
            new XElement("lastName", "Pialorsi"),
            new XElement("addresses",
            new XElement("address",
            new XAttribute("type", "email"),
            "paolo@devleap.it"),
            new XElement("address",
            new XAttribute("type", "url"),
            "http://www.devleap.it/"),
            new XElement("address",
            new XAttribute("type", "home"),
            "Brescia - Italy"))));

            String st = customer.ToString();
            Console.WriteLine(st);


            foreach(XElement a in customer.Descendants("addresses").Elements()) 
            {
                  Console.WriteLine(a);
            }

           

        }

        public static void ComputeAmount()
        {

            XElement order = new XElement("order",
new XElement("quantity", 10),
new XElement("price", 50),
new XAttribute("idProduct", "P01"));
            Decimal orderTotalAmount =
            (Decimal)order.Element("quantity") *
            (Decimal)order.Element("price");
            Console.WriteLine("Order total amount: {0}", orderTotalAmount);
        }

        public static void LINQTOXMLAPI()
        {

            XElement tag = new XElement("customer",
               new XElement("firstName", "Paolo"));

            String s = tag.ToString();
            Console.WriteLine(s);


        }

        public static void LOADFROMXML()
        {
            XElement customers = XElement.Load(@"customer.xml");
            var elements = customers.Elements();

            foreach (XNode a in elements.DescendantsAndSelf().InDocumentOrder()) {
                        Console.WriteLine(a);
            }

            

        }


        public static void LINQXMLQUERY()
        {
            XElement customers = XElement.Load(@"customer.xml");
            var elements = customers.Elements();

            var indians =
             from c in elements
             let xName = (String)c.Attribute("name") 
             let xCity = (String)c.Attribute("city")
             let xCountry =(String)c.Attribute("country")
             where xCountry == "India"
             select new
             {
                 Name = xName,
                 City = xCity
             };

            foreach (var ct in indians)
            {
                Console.WriteLine(ct.Name+ct.City);

            }


        }


        static void Main(string[] args)
        {
            PersistXML();
            ComputeAmount();
            LINQTOXMLAPI();
            LOADFROMXML();
            LINQXMLQUERY();
            Console.ReadLine();
        }
    }
}
