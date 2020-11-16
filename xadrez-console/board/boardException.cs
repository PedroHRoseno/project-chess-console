using System;
using System.Collections.Generic;
using System.Text;

namespace board
{
    class boardException: Exception
    {
        public boardException(string msg): base(msg)
        {
        }
    }
}
