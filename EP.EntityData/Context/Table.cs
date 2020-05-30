using System.Collections.Generic;

namespace OneC.EntityData.Context
{
    public partial class Table
    {        
        public Table()
        {
            TableColumns = new List<TableColumn>();
        }

        public int Id { get; set; }

        public string InitialColumnName { get; set; }

        public virtual ICollection<TableColumn> TableColumns { get; set; }
    }
}
