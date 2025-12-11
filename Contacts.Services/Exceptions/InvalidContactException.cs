using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Exceptions
{
    public class InvalidContactException : Exception
    {
        public InvalidContactException() { }
        public InvalidContactException(string message)
            : base(message) { }
        public InvalidContactException(string message, Exception inner) 
            : base(message, inner) { }
    }
}
