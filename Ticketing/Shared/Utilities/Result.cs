using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Shared.Utilities
{
    public class Result
    {
        public Enums.Status Status { get; set; }
        public string Message { get; set; }
    }
    public class GenericResult<T> : Result
    {
        public T Model { get; set; }
    }
}
