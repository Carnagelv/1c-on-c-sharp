using System.Collections.Generic;

namespace OneC.EntityData.Context
{
    public partial class TableRow
    {        
        public TableRow()
        {
            TableRowItems = new List<TableRowItem>();
        }

        public int Id { get; set; }

        public int TableColumnId { get; set; }
        public virtual TableColumn TableColumn { get; set; }

        public virtual ICollection<TableRowItem> TableRowItems { get; set; }
    }
}
