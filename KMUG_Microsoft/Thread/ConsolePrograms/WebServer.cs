///////////////////////
//
//
//
//
//
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;


class WebServer {

  HttpListener _listener;
  string _baseFolder;

  public WebServer( string uriPrefix , string Base ) {
     System.Threading.ThreadPool.SetMaxThreads(50,100);
     System.Threading.ThreadPool.SetMinThreads(50,50);

     _listener = new HttpListener();
     _listener.Prefixes.Add(uriPrefix);
     _baseFolder = Base;
      
  }

  public void Stop() {
     _listener.Stop();
     

  } 

  public void Start() {

   _listener.Start();

   while ( true ) {
     try {

       HttpListenerContext request = _listener.GetContext();
       ThreadPool.QueueUserWorkItem(ProcessRequest,request);
     }
     catch(Exception e ) {
          Console.WriteLine(e);
          break;

     }


   }

  } 


  void ProcessRequest( object req ) {
    Console.WriteLine("Hello World");


  }

  public static void Main(String [] args ) {
          var server = new WebServer("http://localhost:8080/",@"c:\benoy\WebFolder");
 new System.Threading.Thread( server.Start ).Start();

  Console.WriteLine("Server running .. Press Enter to stop ");
  Console.ReadLine();
  server.Stop();

  }

  

}