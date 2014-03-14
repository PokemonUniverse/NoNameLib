using System;

namespace NoNameLib.Net.Exceptions
{
    public class NetworkingException : NoNameLibException
    {
        public NetworkingException(Enum errorEnumValue) 
            : base(errorEnumValue)
        {
        }

        public NetworkingException(Enum errorEnumValue, string message, params object[] args)
            : base(errorEnumValue, message, args)
        {
        }
    }
}
