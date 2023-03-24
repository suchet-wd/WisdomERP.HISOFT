using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HI.Auditor
{
    [Serializable]
    internal class UnmatchedTypeException : Exception
    {
        public UnmatchedTypeException() : base("The object types do not match") { }
    }
}
