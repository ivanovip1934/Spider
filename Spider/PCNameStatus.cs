using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{
    enum NameState { 
        Current,
        Previous,
        Undefined
    }
    internal class PCNameStatus
    {
        public string Name { get; private set; }
        public NameState State { get; private set; }
        public PCNameStatus(string name, NameState state)
        {
            Name = name;
            State = state;
        }
    }
}
