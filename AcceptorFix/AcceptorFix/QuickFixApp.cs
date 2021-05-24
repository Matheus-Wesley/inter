using QuickFix;
using QuickFix.Fields;
using System;

namespace AcceptorFix
{

    public class QuickFixApp : MessageCracker, IApplication
    {
        
        int orderID = 0;
        int execID = 0;

        private string GenOrderID() { return (++orderID).ToString(); }
        private string GenExecID() { return (++execID).ToString(); }
        private Msgtsl mst = new Msgtsl();


        #region QuickFix.Application Methods
        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("IN: " + message);
            Crack(message, sessionID);
        }

        public void ToApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("OUT: " + message);
        }

        public void FromAdmin(Message message, SessionID sessionID) { }

        public void ToAdmin(Message message, SessionID sessionID)
        {
            Console.WriteLine(mst.Msgtsla(message));
        }
        public void OnCreate(SessionID sessionID) { }
        public void OnLogout(SessionID sessionID) { }
        public void OnLogon(SessionID sessionID) { }
        #endregion
        public void OnMessage(QuickFix.FIX44.NewOrderSingle ord, SessionID sessionID)
        {

            Symbol symbol = ord.Symbol;
            Side side = ord.Side;
            OrdType ordType = ord.OrdType;
            OrderQty orderQty = ord.OrderQty;
            Price price = new Price(10);

            ClOrdID clOrdID = ord.ClOrdID;
            QuickFix.FIX44.ExecutionReport exReport = new QuickFix.FIX44.ExecutionReport(
               new OrderID(GenOrderID()),
               new ExecID(GenExecID()),
               new ExecType(ExecType.FILL),
               new OrdStatus(OrdStatus.FILLED),
               symbol,
               side,
               new LeavesQty(0),
               new CumQty(orderQty.getValue()),
               new AvgPx(price.getValue())
            );
            exReport.ClOrdID = clOrdID;
            exReport.Symbol = symbol;
            exReport.OrderQty = orderQty;
            exReport.LastQty = new LastQty(orderQty.getValue());
            exReport.LastPx = new LastPx(price.getValue());

            if (ord.IsSetAccount())
            {
                exReport.Account = ord.Account;
            }
            try
            {
                Session.SendToTarget(exReport, sessionID);

            }
            catch (SessionNotFound ex)
            {
                Console.WriteLine("==session not found exception!==");
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }

}