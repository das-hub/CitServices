using CitServices.Extensions;

namespace CitServices.PhoneStorage
{
    public class PhoneInfo
    {
        [ExcelColumn("ФИО")] public string Person { get; set; }
        [ExcelColumn("Отдел")] public string Department { get; set; }
        [ExcelColumn("Должность")] public string Position { get; set; }
        [ExcelColumn("Внутренний SIP номер")] public string Sip { get; set; }
        [ExcelColumn("Адрес")] public string Address { get; set; }
        [ExcelColumn("Номер кабинета")] public string Cabinet { get; set; }
        [ExcelColumn("e-mail")] public string Email { get; set; }
        [ExcelColumn("Городской номер")] public string Phone { get; set; }
        [ExcelColumn("Сотовый номер")] public string Mobile { get; set; }
    }
}
