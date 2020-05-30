namespace OneC.EntityData.Context
{
    public partial class TableRowItem
    {        
        public int Id { get; set; }

        public int TableRowId { get; set; }
        public virtual TableRow TableRow { get; set; }

        public int TableColumnId { get; set; }

        public string Value { get; set; }
    }
}
