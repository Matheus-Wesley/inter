using QuickFix;
using System;
using Acceptor;

namespace AcceptorFix
{
    class Program
    {
        private const string HttpServerPrefix = "http://127.0.0.1:5080/";
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Start");
                SessionSettings settings = new SessionSettings("Acceptor.cfg");
                IApplication app = new QuickFixApp();
                IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
                ILogFactory logFactory = new FileLogFactory(settings);
                ThreadedSocketAcceptor acceptor = new ThreadedSocketAcceptor(app, storeFactory, settings, logFactory);
                HttpServer srv = new HttpServer(HttpServerPrefix, settings);

                acceptor.Start();
                srv.Start();
                Console.WriteLine("View Executor status: " + HttpServerPrefix);

                Console.Read();
                acceptor.Stop();
            }
            catch(Exception e)
            {
                Console.WriteLine("FATAL ERROR");
                Console.WriteLine(e.ToString());
            }
        }
    }
}
