using QuickFix;
using System;


namespace AcceptorFix
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Start");
                SessionSettings settings = new SessionSettings("");
                IApplication app = new QuickFixApp();
                IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
                ILogFactory logFactory = new FileLogFactory(settings);
                ThreadedSocketAcceptor acceptor = new ThreadedSocketAcceptor(app, storeFactory, settings, logFactory);

                acceptor.Start();
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
