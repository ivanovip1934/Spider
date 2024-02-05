using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{
    
    enum OnlineState
    {
        Online,
        Offline,
        Undefined
    }
    class PCstatus
    {
        public string DNSName;
        public string IP;
        public OnlineState OnlineStatus;

        public PCstatus(string dnsname, string ip, OnlineState onlineStatus)
        {

            DNSName = dnsname;
            IP = ip;
            OnlineStatus = onlineStatus;
        }

    }
}
