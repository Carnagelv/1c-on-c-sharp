using System.Collections.Generic;

namespace OneC.EntityData.Context
{
    public partial class TableColumn
    {        
        public TableColumn()
        {
            TableRows = new List<TableRow>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }
        public bool IsInitial { get; set; }

        public int TableId { get; set; }
        public virtual Table Table { get; set; }

        public virtual ICollection<TableRow> TableRows { get; set; }
    }
}
