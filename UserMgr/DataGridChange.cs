namespace WinUserMgr
{
    public class DataGridChange
    {
        public object OriginalValue { get; set; }
        public object NewValue { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public string RowFirstColumnValue { get; set; }
        public string Title { get; set; }
        public bool isNewRow { get; set; }
    }
}
