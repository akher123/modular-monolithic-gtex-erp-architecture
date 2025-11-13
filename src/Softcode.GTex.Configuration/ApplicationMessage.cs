using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Configuration
{
    public class ApplicationMessage
    {

        ///0 = Field Name
        public const string RequiredMessage = "{0} is required.";
        //0 = Field Name, 1 = Length
        public static string MaxLengthMessage { get; set; } = "Maximum length of {0} is {1} characters.";
        //0 = Field Name, 1 = Min Value, 2= Max Value
        public string RangeMessage { get; set; } = "{0} must be between {1} and {2}";
    }
}
