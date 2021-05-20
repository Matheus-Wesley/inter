using System;

class Msg
{
    public string BeginString { get; set; }
    public string BodyLenght { get; set; }
    public string MsgType { get; set; }
    public string MsgSeqNum { get; set; }
    public string SenderCompID { get; set; }
    public string SendingTime { get; set; }
    public string TargetCompID { get; set; }
    public string EncryptMethod { get; set; }
    public string HeartBtInt { get; set; }
    public string BeginSeqNo { get; set; }
    public string EndSeqNo { get; set; }
    public string CheckSum { get; set; }
    public string TestReqID { get; set; }
}
