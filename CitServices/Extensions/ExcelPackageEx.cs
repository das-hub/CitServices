using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;

namespace CitServices.Extensions
{
    public class ExcelColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }

        public ExcelColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }

    public static class ExcelPackageEx
    {
        public static ExcelWorksheet GetWorksheet(this ExcelPackage package, string sheetName)
        {
            if (!package.Workbook.Worksheets.Any(ws => ws.Name.Equals(sheetName, StringComparison.CurrentCultureIgnoreCase)))
                throw new FormatException($"Не удалось найти Лист [{sheetName}]");

            return package.Workbook.Worksheets[sheetName];
        }

        public static ExcelCellAddress GetCellAddress(this ExcelWorksheet ws, string value)
        {
            return ws.Cells.First(c =>
                c.GetValue<string>().Equals(value, StringComparison.InvariantCultureIgnoreCase))?.Start;
        }

        public static bool IsValid<T>(this ExcelWorksheet worksheet)
        {
            IEnumerable<PropertyInfo> props = typeof(T).GetPropertiesWithCustomAttributes(typeof(ExcelColumnAttribute));

            foreach (PropertyInfo prop in props) if (worksheet.GetCellAddress(prop.ExcelColumnName()) == null) return false;

            return true;
        }

        public static List<T> ToList<T>(this ExcelWorksheet worksheet) where T : new()
        {
            List<T> result = new List<T>();

            IEnumerable<PropertyInfo> props = typeof(T).GetPropertiesWithCustomAttributes(typeof(ExcelColumnAttribute));

            var start = worksheet.GetCellAddress(props.First().ExcelColumnName());
            var end = worksheet.Dimension.End;

            for (int row = start.Row + 1; row <= end.Row; row++)
            {
                T obj = Activator.CreateInstance<T>();

                foreach (PropertyInfo prop in props)
                {
                    var column = worksheet.GetCellAddress(prop.ExcelColumnName()).Column;
                    prop.SetValue(obj, worksheet.Cells[row, column]?.Text.Trim());
                }                

                result.Add(obj);
            }

            return result;
        }
    }

    public static class TypeEx
    {
        public static IEnumerable<PropertyInfo> GetPropertiesWithCustomAttributes(this Type type, params Type[] typesAttribute)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.HasCustomAttributes(typesAttribute));
        }
    }

    public static class PropertyInfoEx
    {
        public static string ExcelColumnName(this PropertyInfo prop)
        {
            if (prop.GetCustomAttribute(typeof(ExcelColumnAttribute), false) is ExcelColumnAttribute column)
            {
                return column.ColumnName;
            }
            else
            {
                return string.Empty;
            }
        }

        public static bool HasCustomAttributes(this PropertyInfo prop, params Type[] types)
        {
            return prop.GetCustomAttributes().Any(a => types.Contains(a.GetType()));
        }
    }
}
