namespace OneC.EntityData.Context
{
    public partial class TableItem
    {        
        public int Id { get; set; }

        public int TableColumnId { get; set; }
        public virtual TableColumn TableColumn { get; set; }

        public string Value { get; set; }
    }
}
