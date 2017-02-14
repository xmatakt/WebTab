using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace WebTab.Models
{
    public class WebTable : IDisposable
    {
        private readonly DateTime _fromDate;
        private readonly DateTime _toDate;
        private readonly IQueryable<TableItem> _itemsInRange;
        private readonly webtabProjectEntities _db = new webtabProjectEntities();

        public Dictionary<int, string> TableHeaders;
        public List<WebTableRow> TableRows;

        public WebTable(int month)
        {
            _fromDate = new DateTime(DateTime.Now.Year, month, 1);
            _toDate = new DateTime(DateTime.Now.Year, month, DateTime.DaysInMonth(DateTime.Now.Year, month));
            _itemsInRange = _db.TableItems.Where(x => x.ItemDate >= _fromDate &&
                                                    x.ItemDate <= _toDate);

            GetTableHeaders();
            GetTableRows();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        private void GetTableHeaders()
        {
            TableHeaders = new Dictionary<int, string>();

            TableHeaders = _itemsInRange
                .Select(x=>new{ColumnOrder = x.ColumnInfo.ColumnOrder, ColumnName = x.ColumnInfo.ColumnName})
                .Distinct()
                .OrderBy(x => x.ColumnOrder)
                .ToDictionary
                (
                    key => key.ColumnOrder,
                    value => value.ColumnName
                );
        }

        private void GetTableRows()
        {
            var actualDate = new DateTime(1900, 1, 1);
            TableRows = new List<WebTableRow>();
            var orderedItems = _itemsInRange.OrderBy(x => x.ItemDate).ThenBy(x => x.ColumnInfo.ColumnOrder);

            if (orderedItems.FirstOrDefault() == null) return;

            foreach (var item in orderedItems)
            {
                if (item.ItemDate != actualDate)
                {
                    actualDate = item.ItemDate;
                    TableRows.Add(new WebTableRow(actualDate));
                    TableRows.Last().AddRowItem(item.ColumnName, item.ItemValue);
                }
                else
                    TableRows.Last().AddRowItem(item.ColumnName, item.ItemValue);
            }
        }
    }
}