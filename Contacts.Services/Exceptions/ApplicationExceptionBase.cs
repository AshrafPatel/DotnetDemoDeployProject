using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Exceptions
{
    public class ApplicationExceptionBase : Exception
    {
        protected ApplicationExceptionBase(string message)
        : base(message) { }

        protected ApplicationExceptionBase(string message, Exception inner)
            : base(message, inner) { }
    }
}
