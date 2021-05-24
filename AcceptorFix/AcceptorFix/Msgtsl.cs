using QuickFix;
using System.Text.Json;


namespace AcceptorFix
{
    class Msgtsl
    {
        public string Msgtsla(Message msg)
        {
            string[] vMsg = msg.ToString().Split("\x0001");
            string[] vd;
            var msgJ = new Msg();
            foreach (string parte in vMsg)
            {
                if (!string.IsNullOrEmpty(parte))
                {
                    vd = parte.Split("=");
                    switch (vd[0])
                    {
                        case
                            "8":
                            msgJ.BeginString = vd[1];
                            break;
                        case
                            "9":
                            msgJ.BodyLenght = vd[1];
                            break;
                        case
                            "35":
                            msgJ.MsgType = vd[1];
                            break;
                        case
                            "34":
                            msgJ.MsgSeqNum = vd[1];
                            break;
                        case
                            "49":
                            msgJ.SenderCompID = vd[1];
                            break;
                        case
                            "52":
                            msgJ.SendingTime = vd[1];
                            break;
                        case
                            "56":
                            msgJ.TargetCompID = vd[1];
                            break;
                        case
                            "98":
                            msgJ.EncryptMethod = vd[1];
                            break;
                        case
                            "108":
                            msgJ.HeartBtInt = vd[1];
                            break;
                        case
                            "7":
                            msgJ.BeginSeqNo = vd[1];
                            break;
                        case
                            "16":
                            msgJ.EndSeqNo = vd[1];
                            break;
                        case
                            "10":
                            msgJ.CheckSum = vd[1];
                            break;
                        case
                            "112":
                            msgJ.TestReqID = vd[1];
                            break;
                        case
                            "54":
                            msgJ.NewOrderSingle = vd[1];
                            break;
                    }
                }

            }
            string jMsg = JsonSerializer.Serialize(msgJ);
            return jMsg;
        }
    }
}
