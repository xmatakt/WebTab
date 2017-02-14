using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTab.Models
{
    public class WebTableRow
    {
        public Dictionary<string, decimal> RowItems;
        public DateTime ItemDate;

        public WebTableRow(DateTime date)
        {
            ItemDate = date;
            RowItems = new Dictionary<string, decimal>();
        }

        public void AddRowItem(string columnName, decimal value)
        {
            RowItems.Add(columnName, value);
        }

        public decimal this[string columnName]
        {
            get { return RowItems[columnName]; }
            set { RowItems[columnName] = value; }
        }
    }
}