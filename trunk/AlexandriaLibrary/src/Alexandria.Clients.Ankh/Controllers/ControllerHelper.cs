using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Telesophy.Alexandria.Clients.Ankh.Controllers
{
    internal static class ControllerHelper
    {
        internal static string GetPropertyNameFromColumnName(string columnName)
        {
            if (!string.IsNullOrEmpty(columnName))
            {
                string propertyName = columnName;

                if (columnName.Length > 6 && columnName.EndsWith("Column"))
                {
                    propertyName = propertyName.Substring(0, 1).ToUpper() + propertyName.Substring(1, columnName.Length - 7);
                }

                return propertyName;
            }

            return null;
        }

        internal static string GetDefaultOpenDirectory()
        {
            return ConfigurationManager.AppSettings[ControllerConstants.KEY_OPEN_DIR_ROOT];
        }

        internal static int GetMaximumSortColumns()
        {
            return ControllerConstants.MAX_SORT_COLUMNS;
        }
    }
}
