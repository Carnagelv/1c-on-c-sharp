using OneC.BusinessLogic.Models;
using OneC.BusinessLogic.Services;
using OneC.EntityData.Context;
using OneC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace OneC.BusinessLogic.Managers
{
    public interface ITableColumnManager
    {
        bool SaveCatalog(string name);
        List<TableStructureViewModel> GetTable();
        List<TableRowViewModel> GetColumns(int id);
        bool SaveColumn(string name, int tableId, int parentId);
    }

    public class TableColumnManager : ITableColumnManager
    {
        private readonly ITableService _tableService;
        private readonly ITableColumnService _tableColumnService;
        private readonly ITableRowService _tableRowService;
        private readonly ITableRowItemService _tableRowItemService;

        public TableColumnManager
        (
            ITableService tableService,
            ITableColumnService tableColumnService,
            ITableRowService tableRowService,
            ITableRowItemService tableRowItemService
        )
        {
            _tableColumnService = tableColumnService;
            _tableRowService = tableRowService;
            _tableRowItemService = tableRowItemService;
            _tableService = tableService;
        }

        public bool SaveCatalog(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            return _tableColumnService.SaveCatalog(name);
        }

        public List<TableStructureViewModel> GetTable()
        {
            var tables = new List<TableStructureViewModel>();
            var mapper = Mappings.GetMapper();

            var tablesDb = _tableService.GetAll();
            var columnDb = _tableColumnService.GetAll();

            foreach (var table in tablesDb)
            {
                var mappedColumns = mapper.Map<List<ColumnVieModel>>(columnDb);
                var parentColumns = mappedColumns.Where(w => !w.ParentId.HasValue).ToList();

                foreach (var column in parentColumns)
                    column.ChildColumns = mappedColumns.Where(w => w.ParentId == column.Id).ToList();

                tables.Add(new TableStructureViewModel
                {
                    Columns = parentColumns
                });
            }

            var rowsEntities = _tableRowService.GetAll();
            var rowsItemEntities = _tableRowItemService.GetAll();

            foreach (var table in tables)
            {
                var rows = new List<RowViewModel>();

                foreach (var row in rowsEntities)
                {
                    var rowItem = new RowViewModel
                    {
                        RowId = row.Id,
                        TableId = row.TableColumnId
                    };

                    foreach (var column in table.Columns)
                    {
                        if (column.IsInitial && column.ChildColumns.Count == 0)
                        {
                            rowItem.RowItems.Add(new RowItemViewModel
                            {
                                ColumnId = column.Id,
                                Values = rowsItemEntities.Where(w => w.TableColumnId == column.Id && w.TableRowId == row.Id)
                                .Select(s => new RowItemTemplate
                                {
                                    Id = s.Id,
                                    Value = s.Value
                                }).ToList()
                            });
                        }
                        else
                        {
                            foreach (var childColumn in column.ChildColumns)
                            {
                                rowItem.RowItems.Add(new RowItemViewModel
                                {
                                    ColumnId = childColumn.Id,
                                    Values = rowsItemEntities.Where(w => w.TableColumnId == childColumn.Id && w.TableRowId == row.Id)
                                    .Select(s => new RowItemTemplate
                                    {
                                        Id = s.Id,
                                        Value = s.Value
                                    }).ToList()
                                });
                            }
                        }                        
                    }

                    rows.Add(rowItem);
                }

                table.Rows = rows;
            }

            return tables;
        }

        public List<TableRowViewModel> GetColumns(int id)
        {
            var result = new List<TableRowViewModel>();
            var mapper = Mappings.GetMapper();

            var table = _tableService.Get(g => g.Id == id);

            if (table != null)
            {
                var columnDb = _tableColumnService.GetMany(g => g.TableId == id);

                result = mapper.Map<List<TableRowViewModel>>(columnDb);               
            }

            return result;
        }

        public bool SaveColumn(string name, int tableId, int parentId)
        {
            if (parentId != 0)
            {
                if (!_tableColumnService.IsExist(i => i.Name == name && i.ParentId == parentId && i.TableId == tableId))
                {
                    var parent = _tableColumnService.Get(g => g.Id == parentId);

                    parent.IsInitial = true;

                    _tableColumnService.Update(parent);

                    _tableColumnService.Add(new TableColumn
                    {
                        Name = name,
                        ParentId = parentId,
                        TableId = tableId
                    });

                    return true;
                }
            }
            else
            {
                if (!_tableColumnService.IsExist(i => i.Name == name && i.TableId == tableId))
                {
                    _tableColumnService.Add(new TableColumn
                    {
                        Name = name,
                        TableId = tableId,
                        IsInitial = false
                    });

                    return true;
                }
            }

            return false;
        }
    }
}
