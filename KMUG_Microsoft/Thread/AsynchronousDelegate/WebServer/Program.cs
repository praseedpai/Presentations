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


class WebServer
{

    HttpListener _listener;
    string _baseFolder;

    public WebServer(string uriPrefix, string Base)
    {
        System.Threading.ThreadPool.SetMaxThreads(50, 100);
        System.Threading.ThreadPool.SetMinThreads(50, 50);

        _listener = new HttpListener();
        _listener.Prefixes.Add(uriPrefix);
        _baseFolder = Base;

    }

    public void Stop()
    {
        _listener.Stop();


    }

    public void Start()
    {

        _listener.Start();

        while (true)
        {
            try
            {

                HttpListenerContext request = _listener.GetContext();
                ThreadPool.QueueUserWorkItem(ProcessRequest, request);
            }
            catch (HttpListenerException e)
            {
                Console.WriteLine(e);
                break;

            }

            catch (InvalidOperationException e2)
            {

                Console.WriteLine(e2);
                break;
            }


        }

    }


    void ProcessRequest(object req)
    {
        var context = req as HttpListenerContext;
        context.Response.StatusCode = (int)HttpStatusCode.OK;
        String str = "Hello World .............. ";
        byte[] opt = Encoding.UTF8.GetBytes(str);
        context.Response.ContentLength64 = opt.Length;
        Stream s = context.Response.OutputStream;
        s.Write(opt, 0, opt.Length);
        s.Dispose();


    }

    public static void Main(String[] args)
    {
        var server = new WebServer

("http://localhost:8085/", @"c:\benoy\WebFolder");
        new System.Threading.Thread(server.Start).Start();

        Console.WriteLine("Server running .. Press Enter to stop ");
        Console.ReadLine();
        server.Stop();

    }



}