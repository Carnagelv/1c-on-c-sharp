using OneC.BusinessLogic.Services;
using OneC.EntityData.Context;
using OneC.ViewModels;
using System.Collections.Generic;

namespace OneC.BusinessLogic.Managers
{
    public interface ITableRowManager
    {
        bool SaveRow(List<TableRowViewModel> rows, int tableId);
    }

    public class TableRowManager : ITableRowManager
    {
        private readonly ITableRowService _tableRowService;

        public TableRowManager(ITableRowService tableRowService)
        {
            _tableRowService = tableRowService;
        }

        public bool SaveRow(List<TableRowViewModel> rowsItems, int tableId)
        {
            var row = new TableRow
            {
                TableColumnId = tableId
            };

            foreach (var rowItem in rowsItems)
            {
                row.TableRowItems.Add(new TableRowItem
                {
                    TableColumnId = rowItem.Id,
                    Value = rowItem.Value
                });
            }

            _tableRowService.Add(row);

            return true;
        }
    }
}
