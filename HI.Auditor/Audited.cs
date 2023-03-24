using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HI.Auditor
{
    // <summary>
    /// An attribute to decorate properties to be audited
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Audited : Attribute
    {
    }
}
