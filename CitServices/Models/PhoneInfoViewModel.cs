using System.Collections.Generic;
using CitServices.PhoneStorage;

namespace CitServices.Models
{
    public class PhoneInfoViewModel
    {
        public string Header { get; set; }
        public IEnumerable<PhoneInfo> Phones { get; set; }
    }
}
