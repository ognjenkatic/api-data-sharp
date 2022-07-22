using System;
using System.Collections.Generic;
using System.Text;

namespace ApiDataSharp.Util
{
    public class UnsupportedOperatorException : Exception
    {
        public UnsupportedOperatorException(string optor) : base($"No builder found for operation '{optor}'." +
            $"Create one by implementing the IDataSharpExpressionBuilder and add it to the registry")
        {
            
        }
    }
}
