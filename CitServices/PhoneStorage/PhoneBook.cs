using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CitServices.Extensions;
using OfficeOpenXml;

namespace CitServices.PhoneStorage
{
    public class PhoneBook : IPhoneBook
    {
        private readonly string _file;
        private readonly string _sheetName;
        private DateTime _lastCreationTime;
        private List<PhoneInfo> _phones;

        public PhoneBook(string file, string sheetName)
        {
            _file = file;
            _sheetName = sheetName;
            _lastCreationTime = DateTime.MinValue;
        }

        private void LoadAndParseData()
        {
            using (FileStream fs = new FileStream(_file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (ExcelPackage _excel = new ExcelPackage(fs))
                {
                    ExcelWorksheet sheet = _excel.GetWorksheet(_sheetName);

                    if (!sheet.IsValid<PhoneInfo>())
                        throw new FormatException($"Лист [{_sheetName}] не соответствует формату данных. Требуемые колонки отсутствуют.");

                    _phones = sheet.ToList<PhoneInfo>();
                }
            }
        }

        public bool HasChange => _lastCreationTime != CreationTime;

        public DateTime CreationTime => File.GetCreationTime(_file);

        public IEnumerable<PhoneInfo> Phones
        {
            get
            {
                if (HasChange)
                {
                    _lastCreationTime = CreationTime;
                    LoadAndParseData();
                }

                return _phones.OrderBy(p => p.Department);
            }
        }
    }
}
