using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFilter.Exceptions
{
    class FilterErrorStateException : Exception
    {
        public FilterErrorStateException()
        {

        }
        public FilterErrorStateException(string message) : base(message)
        {

        }
         public FilterErrorStateException(string message,Exception inner) :base(message,inner)
        {

        }
    }
}
