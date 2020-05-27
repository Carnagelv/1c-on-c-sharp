using System.Collections.Generic;

namespace OneC.EntityData.Context
{
    public partial class TableColumn
    {        
        public TableColumn()
        {
            TableItems = new List<TableItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public virtual ICollection<TableItem> TableItems { get; set; }
    }
}
