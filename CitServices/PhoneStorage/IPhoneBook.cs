using System.Collections.Generic;

namespace CitServices.PhoneStorage
{
    public interface IPhoneBook
    {
        IEnumerable<PhoneInfo> Phones { get; }
    }
}
