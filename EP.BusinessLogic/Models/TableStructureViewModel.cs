using System.Collections.Generic;

namespace OneC.ViewModels
{
    public class TableStructureViewModel
    {
        public List<ColumnVieModel> Columns { get; set; } = new List<ColumnVieModel>();
    }

    public class ColumnVieModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}