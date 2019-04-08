using System.Collections.Generic;
using System.Linq;
using CitServices.Models;
using CitServices.PhoneStorage;
using Microsoft.AspNetCore.Mvc;

namespace CitServices.Controllers
{
    public class PhoneController : Controller
    {
        private readonly IPhoneBook _phoneBook;

        public PhoneController(IPhoneBook phoneBook)
        {
            _phoneBook = phoneBook;
        }

        public IActionResult List(string department = null)
        {
            PhoneInfoViewModel models = new PhoneInfoViewModel
            {
                Header = department ?? "Все отделы",
                Phones = _phoneBook.Phones
                    .Where(p => department == null || p.Department == department)
            };

            return View(models);
        }
    }
}