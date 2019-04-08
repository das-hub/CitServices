using System.Collections.Generic;
using System.Linq;
using CitServices.PhoneStorage;
using Microsoft.AspNetCore.Mvc;

namespace CitServices.Components
{
    public class Departments : ViewComponent
    {
        public readonly IPhoneBook _phoneBook;

        public Departments(IPhoneBook phoneBook)
        {            
            _phoneBook = phoneBook;
        }

        public IViewComponentResult Invoke()
        {
            string department = RouteData.Values["department"] as string;

            ViewBag.SelectedDepartment = department ?? "Отделы";
            IEnumerable<string> departments = _phoneBook.Phones
                                                        .Select(p => p.Department)
                                                        .Distinct().OrderBy(d => d);
            return View(departments);
        }
    }
}
