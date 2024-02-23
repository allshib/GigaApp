using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Exceptions
{
    public static class ValidationErrorCode
    {
        public const string Empty = nameof(Empty);
        public const string TooLong = nameof(TooLong);
        public const string Invalid = nameof(Invalid);
    }
}
