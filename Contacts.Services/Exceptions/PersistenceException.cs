using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Exceptions
{
    public class PersistenceException : Exception
    {
        public PersistenceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
