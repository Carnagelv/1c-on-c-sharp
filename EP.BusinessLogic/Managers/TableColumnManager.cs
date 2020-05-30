using OneC.BusinessLogic.Models;
using OneC.BusinessLogic.Services;
using OneC.EntityData.Context;
using OneC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneC.BusinessLogic.Managers
{
    public interface ITableColumnManager
    {
        bool SaveCatalog(string name);
        List<TableStructureViewModel> GetTable();
    }

    public class TableColumnManager : ITableColumnManager
    {
        private readonly ITableColumnService _tableColumnService;

        public TableColumnManager(ITableColumnService tableColumnService)
        {
            _tableColumnService = tableColumnService;
        }

        public bool SaveCatalog(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            return _tableColumnService.SaveCatalog(name);
        }

        public List<TableStructureViewModel> GetTable()
        {
            var tablesResult = new List<TableStructureViewModel>();
            var mapper = Mappings.GetMapper();

            var entities = _tableColumnService.GetColumns();
            var tables = entities.Where(w => !w.ParentId.HasValue).ToList();

            foreach (var table in tables)
            {
                var columns = new List<TableColumn>{ table };
                columns.AddRange(GetChildColumn(columns, entities));

                tablesResult.Add(new TableStructureViewModel
                {
                    Columns = mapper.Map<List<ColumnVieModel>>(columns)
                });
            }


            return tablesResult;
        }

        private List<TableColumn> GetChildColumn(List<TableColumn> columns, List<TableColumn> entities)
        {
            var resultColumn = new List<TableColumn>();

            foreach (var column in columns)
            {
                var childColumn = entities.Where(w => w.ParentId == column.Id).ToList();
                childColumn.AddRange(GetChildColumn(childColumn, entities));

                resultColumn.AddRange(childColumn);
            }

            return resultColumn;
        }
    }
}
