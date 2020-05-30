using System.Collections.Generic;

namespace OneC.ViewModels
{
    public class TableStructureViewModel
    {
        public List<ColumnVieModel> Columns { get; set; } = new List<ColumnVieModel>();
        public List<RowViewModel> Rows { get; set; } = new List<RowViewModel>();
    }

    public class ColumnVieModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool IsInitial { get; set; }
        public List<ColumnVieModel> ChildColumns { get; set; } = new List<ColumnVieModel>();
    }

    public class TableRowViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class RowViewModel
    {
        public int RowId { get; set; }
        public int TableId { get; set; }
        public List<RowItemViewModel> RowItems { get; set; } = new List<RowItemViewModel>();
    }

    public class RowItemViewModel
    {
        public int ColumnId { get; set; }
        public List<RowItemTemplate> Values { get; set; } = new List<RowItemTemplate>();
    }

    public class RowItemTemplate
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}